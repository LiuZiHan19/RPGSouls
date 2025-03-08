using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryConfigurationData",
    menuName = "Scriptable Object/Inventory/InventoryConfigurationData")]
public class InventoryConfigurationSO : ScriptableObject
{
    public List<InventoryItemBaseSO> equipmentList = new List<InventoryItemBaseSO>();
    public List<InventoryItemBaseSO> consumableList = new List<InventoryItemBaseSO>();
    public List<InventoryItemBaseSO> materialList = new List<InventoryItemBaseSO>();
    public List<InventoryItemBaseSO> itemList = new List<InventoryItemBaseSO>();
}