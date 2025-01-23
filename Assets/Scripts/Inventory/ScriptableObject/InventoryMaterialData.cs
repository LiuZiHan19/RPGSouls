using UnityEngine;

[CreateAssetMenu(fileName = "InventoryEquipmentData", menuName = "Scriptable Object/Inventory/InventoryMaterialData",
    order = 3)]
public class InventoryMaterialData : InventoryItemBaseData
{
    public InventoryMaterialType materialType;
}