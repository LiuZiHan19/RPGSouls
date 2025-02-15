using System;
using System.Collections.Generic;

public class Inventory : Singleton<Inventory>
{
    public Dictionary<E_InventoryEquipment, InventoryItem> equipments;
    public Dictionary<E_InventoryConsumable, InventoryItem> consumables;
    public Dictionary<E_InventoryMaterial, InventoryItem> materials;
    public Dictionary<E_InventoryItem, InventoryItem> items;

    public void AddItem(InventoryItemBaseSO itemSo)
    {
        switch (itemSo.itemBaseType)
        {
            case E_InventoryItemBase.Equipment:
                InventoryEquipmentSO inventoryEquipmentSo = itemSo as InventoryEquipmentSO;
                equipments[inventoryEquipmentSo.equipmentType].Add();
                break;
            case E_InventoryItemBase.Consumable:
                InventoryConsumableSO inventoryConsumableSo = itemSo as InventoryConsumableSO;
                consumables[inventoryConsumableSo.consumableType].Add();
                break;
            case E_InventoryItemBase.Material:
                InventoryMaterialSO inventoryMaterialSo = itemSo as InventoryMaterialSO;
                materials[inventoryMaterialSo.materialType].Add();
                break;
            case E_InventoryItemBase.Item:
                InventoryItemSO inventoryItemSo = itemSo as InventoryItemSO;
                items[inventoryItemSo.itemType].Add();
                break;
            default:
                Debugger.Error($"[Inventory Add Item Error] 无效的物品类型: {itemSo.itemBaseType}. " +
                             $"物品名称: {itemSo.name}, 物品ID: {itemSo.id}, " +
                             $"物品类型的枚举值: {Enum.GetName(typeof(E_InventoryItemBase), itemSo.itemBaseType)}. " +
                             "请检查该物品的类型和数据设置。");
                break;
        }
    }
}