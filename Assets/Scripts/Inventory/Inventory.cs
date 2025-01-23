using System;
using System.Collections.Generic;

public class Inventory : Singleton<Inventory>
{
    public Dictionary<InventoryEquipmentType, InventoryItem> equipments;
    public Dictionary<InventoryConsumableType, InventoryItem> consumables;
    public Dictionary<InventoryMaterialType, InventoryItem> materials;
    public Dictionary<InventoryItemType, InventoryItem> items;

    public void AddItem(InventoryItemBaseData itemData)
    {
        switch (itemData.itemBaseType)
        {
            case InventoryItemBaseType.Equipment:
                InventoryEquipmentData inventoryEquipmentData = itemData as InventoryEquipmentData;
                equipments[inventoryEquipmentData.equipmentType].Add();
                break;
            case InventoryItemBaseType.Consumable:
                InventoryConsumableData inventoryConsumableData = itemData as InventoryConsumableData;
                consumables[inventoryConsumableData.consumableType].Add();
                break;
            case InventoryItemBaseType.Material:
                InventoryMaterialData inventoryMaterialData = itemData as InventoryMaterialData;
                materials[inventoryMaterialData.materialType].Add();
                break;
            case InventoryItemBaseType.Item:
                InventoryItemData inventoryItemData = itemData as InventoryItemData;
                items[inventoryItemData.itemType].Add();
                break;
            default:
                Logger.Error($"[Inventory Add Item Error] 无效的物品类型: {itemData.itemBaseType}. " +
                             $"物品名称: {itemData.name}, 物品ID: {itemData.id}, " +
                             $"物品类型的枚举值: {Enum.GetName(typeof(InventoryItemBaseType), itemData.itemBaseType)}. " +
                             "请检查该物品的类型和数据设置。");
                break;
        }
    }
}