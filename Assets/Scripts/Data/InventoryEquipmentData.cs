using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "InventoryEquipmentData", menuName = "Scriptable Object/Inventory/InventoryEquipmentData")]
public class InventoryEquipmentData : InventoryItemStatData
{
    [FormerlySerializedAs("equipmentType")] public InventoryEquipmentID equipmentID;

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
        name = equipmentID.ToString();
        string assetPath = AssetDatabase.GetAssetPath(this);
        EditorApplication.delayCall += () =>
        {
            AssetDatabase.RenameAsset(assetPath, name);
            AssetDatabase.SaveAssets();
        };
    }
#endif
}