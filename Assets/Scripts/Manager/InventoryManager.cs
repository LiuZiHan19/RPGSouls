using System;
using System.Collections.Generic;
using System.Linq;

public class InventoryManager : MonoSingleton<InventoryManager>
{
    public Dictionary<E_InventoryEquipment, InventoryItem> equipmentDict;
    public Dictionary<E_InventoryConsumable, InventoryItem> consumableDict;
    public Dictionary<E_InventoryMaterial, InventoryItem> materialDict;
    public Dictionary<E_InventoryItem, InventoryItem> itemDict;
    public List<InventoryItem> allItemList = new List<InventoryItem>();
    public InventoryEquipmentData weapon;

    protected override void Awake()
    {
        base.Awake();
        equipmentDict = new Dictionary<E_InventoryEquipment, InventoryItem>();
        consumableDict = new Dictionary<E_InventoryConsumable, InventoryItem>();
        materialDict = new Dictionary<E_InventoryMaterial, InventoryItem>();
        itemDict = new Dictionary<E_InventoryItem, InventoryItem>();
        GameEventDispatcher.OnInventoryRealItemPickup += AddItemByItemSO;
        GameEventDispatcher.Equip += Equip;
        GameEventDispatcher.UnEquip += UnEquip;

        GameDataManager.Instance.InventoryDataModel.PareSelf();
    }

    private void Equip(InventoryItemBaseData itemSO)
    {
        switch (itemSO.itemBaseType)
        {
            case E_InventoryItemBase.Equipment:
                if (weapon != null) GameEventDispatcher.UnEquip?.Invoke(weapon);
                weapon = itemSO as InventoryEquipmentData;
                RemoveItemByItemSO(itemSO);
                break;
        }
    }

    private void UnEquip(InventoryItemBaseData itemSO)
    {
        switch (itemSO.itemBaseType)
        {
            case E_InventoryItemBase.Equipment:
                AddItemByItemSO(itemSO);
                break;
        }
    }

    public void AddItemByItemSO(InventoryItemBaseData itemSO)
    {
        switch (itemSO.itemBaseType)
        {
            case E_InventoryItemBase.Equipment:
                InventoryEquipmentData inventoryEquipmentSo = itemSO as InventoryEquipmentData;
                if (equipmentDict.Keys.Contains(inventoryEquipmentSo.equipmentType))
                {
                    equipmentDict[inventoryEquipmentSo.equipmentType].Add();
                }
                else
                {
                    equipmentDict.Add(inventoryEquipmentSo.equipmentType, new InventoryItem(inventoryEquipmentSo));
                }

                break;
            case E_InventoryItemBase.Consumable:
                InventoryConsumableData inventoryConsumableSo = itemSO as InventoryConsumableData;
                if (consumableDict.Keys.Contains(inventoryConsumableSo.consumableType))
                {
                    consumableDict[inventoryConsumableSo.consumableType].Add();
                }
                else
                {
                    consumableDict.Add(inventoryConsumableSo.consumableType, new InventoryItem(inventoryConsumableSo));
                }

                break;
            case E_InventoryItemBase.Material:
                InventoryMaterialData inventoryMaterialSo = itemSO as InventoryMaterialData;
                if (materialDict.Keys.Contains(inventoryMaterialSo.materialType))
                {
                    materialDict[inventoryMaterialSo.materialType].Add();
                }
                else
                {
                    materialDict.Add(inventoryMaterialSo.materialType, new InventoryItem(inventoryMaterialSo));
                }

                break;
            case E_InventoryItemBase.Item:
                InventoryItemData inventoryItemSo = itemSO as InventoryItemData;
                if (itemDict.Keys.Contains(inventoryItemSo.itemType))
                {
                    itemDict[inventoryItemSo.itemType].Add();
                }
                else
                {
                    itemDict.Add(inventoryItemSo.itemType, new InventoryItem(inventoryItemSo));
                }

                break;
            default:
                Debugger.Error($"[Inventory Add Item Error] 无效的物品类型: {itemSO.itemBaseType}. " +
                               $"物品名称: {itemSO.name}, 物品ID: {itemSO.id}, " +
                               $"物品类型的枚举值: {Enum.GetName(typeof(E_InventoryItemBase), itemSO.itemBaseType)}. " +
                               "请检查该物品的类型和数据设置。");
                break;
        }

        AddToList(itemSO);
    }

    public void RemoveItemByItemSO(InventoryItemBaseData itemSO)
    {
        switch (itemSO.itemBaseType)
        {
            case E_InventoryItemBase.Equipment:
                if (itemSO is InventoryEquipmentData equipmentSO)
                {
                    if (equipmentDict.TryGetValue(equipmentSO.equipmentType, out var equipmentItem))
                    {
                        equipmentItem.Remove();
                        if (equipmentItem.number <= 0)
                        {
                            equipmentDict.Remove(equipmentSO.equipmentType);
                        }
                    }
                }

                break;
            case E_InventoryItemBase.Consumable:
                if (itemSO is InventoryConsumableData consumableSO)
                {
                    if (consumableDict.TryGetValue(consumableSO.consumableType, out var consumableItem))
                    {
                        consumableItem.Remove();
                        if (consumableItem.number <= 0)
                        {
                            consumableDict.Remove(consumableSO.consumableType);
                        }
                    }
                }

                break;
            case E_InventoryItemBase.Material:
                if (itemSO is InventoryMaterialData materialSO)
                {
                    if (materialDict.TryGetValue(materialSO.materialType, out var materialItem))
                    {
                        materialItem.Remove();
                        if (materialItem.number <= 0)
                        {
                            materialDict.Remove(materialSO.materialType);
                        }
                    }
                }

                break;
            case E_InventoryItemBase.Item:
                if (itemSO is InventoryItemData itemTypeSO)
                {
                    if (itemDict.TryGetValue(itemTypeSO.itemType, out var typeItem))
                    {
                        typeItem.Remove();
                        if (typeItem.number <= 0)
                        {
                            itemDict.Remove(itemTypeSO.itemType);
                        }
                    }
                }

                break;
            default:
                Debugger.Error($"[Inventory Remove Item Error] 无效的物品类型: {itemSO.itemBaseType}. " +
                               $"物品名称: {itemSO.name}, 物品ID: {itemSO.id}");
                break;
        }

        RemoveFromList(itemSO);
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
        List<InventoryItemBaseData> configurationData = GameResources.Instance.inventoryConfigurationSO.equipmentList;

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
        GameEventDispatcher.OnInventoryRealItemPickup -= AddItemByItemSO;
        GameEventDispatcher.Equip -= Equip;
        GameEventDispatcher.UnEquip -= UnEquip;
    }
}