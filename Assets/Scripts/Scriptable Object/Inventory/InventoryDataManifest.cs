using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryConfigurationData",
    menuName = "Scriptable Object/Inventory/InventoryConfigurationData")]
public class InventoryDataManifest : ScriptableObject
{
    public List<InventoryItemBaseData> equipmentList = new List<InventoryItemBaseData>();
    public List<InventoryItemBaseData> consumableList = new List<InventoryItemBaseData>();
    public List<InventoryItemBaseData> materialList = new List<InventoryItemBaseData>();
    public List<InventoryItemBaseData> itemList = new List<InventoryItemBaseData>();
}