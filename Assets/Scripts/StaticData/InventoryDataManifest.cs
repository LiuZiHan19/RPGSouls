using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryDataManifest", menuName = "Scriptable Object/Inventory/InventoryDataManifest")]
public class InventoryDataManifest : ScriptableObject
{
    public List<InventoryItemBaseData> equipmentDataList;
}