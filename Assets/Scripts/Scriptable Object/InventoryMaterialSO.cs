using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryEquipmentData", menuName = "Scriptable Object/Inventory/InventoryMaterialData")]
public class InventoryMaterialSO : InventoryItemBaseSO
{
    public E_InventoryMaterial materialType;

    private void OnValidate()
    {
#if UNITY_EDITOR
        name = materialType.ToString();
        string assetPath = AssetDatabase.GetAssetPath(this);
        EditorApplication.delayCall += () =>
        {
            AssetDatabase.RenameAsset(assetPath, name);
            AssetDatabase.SaveAssets();
        };
#endif
    }
}