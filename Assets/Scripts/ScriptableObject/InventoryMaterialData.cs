using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryEquipmentData", menuName = "Scriptable Object/Inventory/InventoryMaterialData")]
public class InventoryMaterialData : InventoryItemBaseData
{
    public InventoryMaterialType materialType;

    private void OnValidate()
    {
        name = materialType.ToString();
        string assetPath = AssetDatabase.GetAssetPath(this);
        EditorApplication.delayCall += () =>
        {
            AssetDatabase.RenameAsset(assetPath, name);
            AssetDatabase.SaveAssets();
        };
    }
}