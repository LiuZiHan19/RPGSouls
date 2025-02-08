using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryEquipmentData", menuName = "Scriptable Object/Inventory/InventoryItemData")]
public class InventoryItemSO : InventoryItemBaseSO
{
    public E_InventoryItem itemType;

    private void OnValidate()
    {
#if UNITY_EDITOR
        name = itemType.ToString();
        string assetPath = AssetDatabase.GetAssetPath(this);
        EditorApplication.delayCall += () =>
        {
            AssetDatabase.RenameAsset(assetPath, name);
            AssetDatabase.SaveAssets();
        };
#endif
    }
}