using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "InventoryEquipmentData", menuName = "Scriptable Object/Inventory/InventoryConsumableData")]
public class InventoryConsumableData : InventoryItemStatData
{
    [FormerlySerializedAs("consumableType")] public InventoryConsumableID consumableID;

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
        name = consumableID.ToString();
        string assetPath = AssetDatabase.GetAssetPath(this);
        EditorApplication.delayCall += () =>
        {
            AssetDatabase.RenameAsset(assetPath, name);
            AssetDatabase.SaveAssets();
        };
    }
#endif
}