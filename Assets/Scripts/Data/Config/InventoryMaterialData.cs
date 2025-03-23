using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryEquipmentData", menuName = "Scriptable Object/Inventory/InventoryMaterialData")]
public class InventoryMaterialData : InventoryItemBaseData
{
    public E_InventoryMaterial materialType;

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
        name = materialType.ToString();
        string assetPath = AssetDatabase.GetAssetPath(this);
        EditorApplication.delayCall += () =>
        {
            AssetDatabase.RenameAsset(assetPath, name);
            AssetDatabase.SaveAssets();
        };
    }
#endif
}