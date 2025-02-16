using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryEquipmentData", menuName = "Scriptable Object/Inventory/InventoryEquipmentData")]
public class InventoryEquipmentSO : InventoryItemStatSO
{
    public E_InventoryEquipment equipmentType;

    private void OnValidate()
    {
#if UNITY_EDITOR
        name = equipmentType.ToString();
        string assetPath = AssetDatabase.GetAssetPath(this);
        EditorApplication.delayCall += () =>
        {
            AssetDatabase.RenameAsset(assetPath, name);
            AssetDatabase.SaveAssets();
        };
#endif
    }
}