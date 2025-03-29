using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "InventoryEquipmentData", menuName = "Scriptable Object/Inventory/InventoryMaterialData")]
public class InventoryMaterialData : InventoryItemBaseData
{
    [FormerlySerializedAs("materialType")] public InventoryMaterialID materialID;

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
        name = materialID.ToString();
        string assetPath = AssetDatabase.GetAssetPath(this);
        EditorApplication.delayCall += () =>
        {
            AssetDatabase.RenameAsset(assetPath, name);
            AssetDatabase.SaveAssets();
        };
    }
#endif
}