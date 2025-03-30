using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryConfigurationData",
    menuName = "Scriptable Object/Inventory/InventoryConfigurationData")]
public class InventoryDataManifest : ScriptableObject
{
    public List<InventoryItemBaseData> equipmentDataList;
    public List<InventoryItemBaseData> consumableDataList;
    public List<InventoryItemBaseData> materialDataList;
    public List<InventoryItemBaseData> itemDataList;
}