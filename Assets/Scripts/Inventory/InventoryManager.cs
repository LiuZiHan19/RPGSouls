using System;
using System.Collections.Generic;
using System.Linq;

public class InventoryManager : MonoSingleton<InventoryManager>, IDisposable
{
    public Dictionary<E_InventoryEquipment, InventoryItem> equipments;
    public Dictionary<E_InventoryConsumable, InventoryItem> consumables;
    public Dictionary<E_InventoryMaterial, InventoryItem> materials;
    public Dictionary<E_InventoryItem, InventoryItem> items;
    public List<InventoryItem> allItems = new List<InventoryItem>();
    public InventoryEquipmentSO currentWeapon;

    protected override void Awake()
    {
        base.Awake();
        equipments = new Dictionary<E_InventoryEquipment, InventoryItem>();
        consumables = new Dictionary<E_InventoryConsumable, InventoryItem>();
        materials = new Dictionary<E_InventoryMaterial, InventoryItem>();
        items = new Dictionary<E_InventoryItem, InventoryItem>();
        EventDispatcher.OnInventoryRealItemPickup += AddItemByItemSO;
        EventDispatcher.Equip += Equip;
        EventDispatcher.UnEquip += UnEquip;
    }

    private void Equip(InventoryItemBaseSO itemSO)
    {
        switch (itemSO.itemBaseType)
        {
            case E_InventoryItemBase.Equipment:
                if (currentWeapon != null) EventDispatcher.UnEquip?.Invoke(currentWeapon);
                currentWeapon = itemSO as InventoryEquipmentSO;
                RemoveItemByItemSO(itemSO);
                break;
        }
    }

    private void UnEquip(InventoryItemBaseSO itemSO)
    {
        switch (itemSO.itemBaseType)
        {
            case E_InventoryItemBase.Equipment:
                AddItemByItemSO(itemSO);
                break;
        }
    }

    public void AddItemByItemSO(InventoryItemBaseSO itemSO)
    {
        switch (itemSO.itemBaseType)
        {
            case E_InventoryItemBase.Equipment:
                InventoryEquipmentSO inventoryEquipmentSo = itemSO as InventoryEquipmentSO;
                if (equipments.Keys.Contains(inventoryEquipmentSo.equipmentType))
                {
                    equipments[inventoryEquipmentSo.equipmentType].Add();
                }
                else
                {
                    equipments.Add(inventoryEquipmentSo.equipmentType, new InventoryItem(inventoryEquipmentSo));
                }

                break;
            case E_InventoryItemBase.Consumable:
                InventoryConsumableSO inventoryConsumableSo = itemSO as InventoryConsumableSO;
                if (consumables.Keys.Contains(inventoryConsumableSo.consumableType))
                {
                    consumables[inventoryConsumableSo.consumableType].Add();
                }
                else
                {
                    consumables.Add(inventoryConsumableSo.consumableType, new InventoryItem(inventoryConsumableSo));
                }

                break;
            case E_InventoryItemBase.Material:
                InventoryMaterialSO inventoryMaterialSo = itemSO as InventoryMaterialSO;
                if (materials.Keys.Contains(inventoryMaterialSo.materialType))
                {
                    materials[inventoryMaterialSo.materialType].Add();
                }
                else
                {
                    materials.Add(inventoryMaterialSo.materialType, new InventoryItem(inventoryMaterialSo));
                }

                break;
            case E_InventoryItemBase.Item:
                InventoryItemSO inventoryItemSo = itemSO as InventoryItemSO;
                if (items.Keys.Contains(inventoryItemSo.itemType))
                {
                    items[inventoryItemSo.itemType].Add();
                }
                else
                {
                    items.Add(inventoryItemSo.itemType, new InventoryItem(inventoryItemSo));
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

    public void RemoveItemByItemSO(InventoryItemBaseSO itemSO)
    {
        switch (itemSO.itemBaseType)
        {
            case E_InventoryItemBase.Equipment:
                if (itemSO is InventoryEquipmentSO equipmentSO)
                {
                    if (equipments.TryGetValue(equipmentSO.equipmentType, out var equipmentItem))
                    {
                        equipmentItem.Remove();
                        if (equipmentItem.number <= 0)
                        {
                            equipments.Remove(equipmentSO.equipmentType);
                        }
                    }
                }

                break;
            case E_InventoryItemBase.Consumable:
                if (itemSO is InventoryConsumableSO consumableSO)
                {
                    if (consumables.TryGetValue(consumableSO.consumableType, out var consumableItem))
                    {
                        consumableItem.Remove();
                        if (consumableItem.number <= 0)
                        {
                            consumables.Remove(consumableSO.consumableType);
                        }
                    }
                }

                break;
            case E_InventoryItemBase.Material:
                if (itemSO is InventoryMaterialSO materialSO)
                {
                    if (materials.TryGetValue(materialSO.materialType, out var materialItem))
                    {
                        materialItem.Remove();
                        if (materialItem.number <= 0)
                        {
                            materials.Remove(materialSO.materialType);
                        }
                    }
                }

                break;
            case E_InventoryItemBase.Item:
                if (itemSO is InventoryItemSO itemTypeSO)
                {
                    if (items.TryGetValue(itemTypeSO.itemType, out var typeItem))
                    {
                        typeItem.Remove();
                        if (typeItem.number <= 0)
                        {
                            items.Remove(itemTypeSO.itemType);
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

    private void AddToList(InventoryItemBaseSO itemSO)
    {
        foreach (var item in allItems)
        {
            if (item.itemSO == itemSO)
            {
                item.Add();
                return;
            }
        }

        allItems.Add(new InventoryItem(itemSO));
    }

    private void RemoveFromList(InventoryItemBaseSO itemSO)
    {
        foreach (var item in allItems)
        {
            if (item.itemSO == itemSO)
            {
                item.Remove();
                if (item.number == 0) allItems.Remove(item);
                return;
            }
        }
    }

    public void Dispose()
    {
        EventDispatcher.OnInventoryRealItemPickup -= AddItemByItemSO;
        EventDispatcher.Equip -= Equip;
        EventDispatcher.UnEquip -= UnEquip;
    }
}