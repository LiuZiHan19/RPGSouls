using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager m_instance;
    public static InventoryManager Instance => m_instance;

    public Dictionary<InventoryEquipmentID, InventoryItem> equipmentDict;
    public Dictionary<InventoryConsumableID, InventoryItem> consumableDict;
    public Dictionary<InventoryMaterialID, InventoryItem> materialDict;
    public Dictionary<InventoryItemID, InventoryItem> itemDict;
    public List<InventoryItem> allItemList = new List<InventoryItem>();
    public InventoryEquipmentData currentWeaponData;

    private void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        equipmentDict = new Dictionary<InventoryEquipmentID, InventoryItem>();
        consumableDict = new Dictionary<InventoryConsumableID, InventoryItem>();
        materialDict = new Dictionary<InventoryMaterialID, InventoryItem>();
        itemDict = new Dictionary<InventoryItemID, InventoryItem>();

        GameEventDispatcher.OnInventoryRealItemPickup += AddItemByItemData;
        GameEventDispatcher.Equip += Equip;
        GameEventDispatcher.UnEquip += UnEquip;

        GameDataManager.Instance.InventoryDataModel.PareSelf();
    }

    private void Equip(InventoryItemBaseData itemData)
    {
        switch (itemData.itemBaseType)
        {
            case InventoryItemBaseType.Equipment:
                if (currentWeaponData != null)
                    GameEventDispatcher.UnEquip?.Invoke(currentWeaponData);
                currentWeaponData = itemData as InventoryEquipmentData;
                RemoveItemByItemData(itemData);
                break;
        }
    }

    private void UnEquip(InventoryItemBaseData itemData)
    {
        switch (itemData.itemBaseType)
        {
            case InventoryItemBaseType.Equipment:
                AddItemByItemData(itemData);
                break;
        }
    }

    public void AddItemByItemData(InventoryItemBaseData itemData)
    {
        switch (itemData.itemBaseType)
        {
            case InventoryItemBaseType.Equipment:
                InventoryEquipmentData inventoryEquipmentSo = itemData as InventoryEquipmentData;
                if (equipmentDict.Keys.Contains(inventoryEquipmentSo.equipmentID))
                {
                    equipmentDict[inventoryEquipmentSo.equipmentID].Add();
                }
                else
                {
                    equipmentDict.Add(inventoryEquipmentSo.equipmentID, new InventoryItem(inventoryEquipmentSo));
                }

                break;
            case InventoryItemBaseType.Consumable:
                InventoryConsumableData inventoryConsumableSo = itemData as InventoryConsumableData;
                if (consumableDict.Keys.Contains(inventoryConsumableSo.consumableID))
                {
                    consumableDict[inventoryConsumableSo.consumableID].Add();
                }
                else
                {
                    consumableDict.Add(inventoryConsumableSo.consumableID, new InventoryItem(inventoryConsumableSo));
                }

                break;
            case InventoryItemBaseType.Material:
                InventoryMaterialData inventoryMaterialSo = itemData as InventoryMaterialData;
                if (materialDict.Keys.Contains(inventoryMaterialSo.materialID))
                {
                    materialDict[inventoryMaterialSo.materialID].Add();
                }
                else
                {
                    materialDict.Add(inventoryMaterialSo.materialID, new InventoryItem(inventoryMaterialSo));
                }

                break;
            case InventoryItemBaseType.Item:
                InventoryItemData inventoryItemSo = itemData as InventoryItemData;
                if (itemDict.Keys.Contains(inventoryItemSo.itemID))
                {
                    itemDict[inventoryItemSo.itemID].Add();
                }
                else
                {
                    itemDict.Add(inventoryItemSo.itemID, new InventoryItem(inventoryItemSo));
                }

                break;
            default:
                Debugger.Error($"[Inventory Add Item Error] 无效的物品类型: {itemData.itemBaseType}. " +
                               $"物品名称: {itemData.name}, 物品ID: {itemData.id}, " +
                               $"物品类型的枚举值: {Enum.GetName(typeof(InventoryItemBaseType), itemData.itemBaseType)}. " +
                               "请检查该物品的类型和数据设置。");
                break;
        }

        AddToList(itemData);
    }

    public void RemoveItemByItemData(InventoryItemBaseData itemData)
    {
        switch (itemData.itemBaseType)
        {
            case InventoryItemBaseType.Equipment:
                if (itemData is InventoryEquipmentData equipmentItemData)
                {
                    if (equipmentDict.TryGetValue(equipmentItemData.equipmentID, out var equipmentItem))
                    {
                        equipmentItem.Remove();
                        if (equipmentItem.number <= 0)
                        {
                            equipmentDict.Remove(equipmentItemData.equipmentID);
                        }
                    }
                }

                break;
            case InventoryItemBaseType.Consumable:
                if (itemData is InventoryConsumableData consumableItemData)
                {
                    if (consumableDict.TryGetValue(consumableItemData.consumableID, out var consumableItem))
                    {
                        consumableItem.Remove();
                        if (consumableItem.number <= 0)
                        {
                            consumableDict.Remove(consumableItemData.consumableID);
                        }
                    }
                }

                break;
            case InventoryItemBaseType.Material:
                if (itemData is InventoryMaterialData materialItemData)
                {
                    if (materialDict.TryGetValue(materialItemData.materialID, out var materialItem))
                    {
                        materialItem.Remove();
                        if (materialItem.number <= 0)
                        {
                            materialDict.Remove(materialItemData.materialID);
                        }
                    }
                }

                break;
            case InventoryItemBaseType.Item:
                if (itemData is InventoryItemData itemItemData)
                {
                    if (itemDict.TryGetValue(itemItemData.itemID, out var typeItem))
                    {
                        typeItem.Remove();
                        if (typeItem.number <= 0)
                        {
                            itemDict.Remove(itemItemData.itemID);
                        }
                    }
                }

                break;
            default:
                Debugger.Error($"[Inventory Remove Item Error] 无效的物品类型: {itemData.itemBaseType}. " +
                               $"物品名称: {itemData.name}, 物品ID: {itemData.id}");
                break;
        }

        RemoveFromList(itemData);
    }

    private void AddToList(InventoryItemBaseData itemSO)
    {
        foreach (var item in allItemList)
        {
            if (item.itemSO == itemSO)
            {
                item.Add();
                return;
            }
        }

        allItemList.Add(new InventoryItem(itemSO));
    }

    private void RemoveFromList(InventoryItemBaseData itemSO)
    {
        foreach (var item in allItemList)
        {
            if (item.itemSO == itemSO)
            {
                item.Remove();
                if (item.number == 0)
                {
                    allItemList.Remove(item);
                }

                return;
            }
        }
    }

    public InventoryItemBaseData LoadDataByGUID(string guid)
    {
        List<InventoryItemBaseData> configurationData = GameResources.Instance.InventoryDataManifest.equipmentList;

        foreach (var itemData in configurationData)
        {
            if (guid == itemData.id)
            {
                return itemData;
            }
        }

        Debugger.Error($"[Inventory Load Data Error] 无法找到物品: {guid}");
        return null;
    }

    ~InventoryManager()
    {
        GameEventDispatcher.OnInventoryRealItemPickup -= AddItemByItemData;
        GameEventDispatcher.Equip -= Equip;
        GameEventDispatcher.UnEquip -= UnEquip;
    }
}