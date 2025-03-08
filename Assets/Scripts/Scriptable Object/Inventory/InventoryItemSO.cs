using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryEquipmentData", menuName = "Scriptable Object/Inventory/InventoryItemData")]
public class InventoryItemSO : InventoryItemBaseSO
{
    public E_InventoryItem itemType;

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
        name = itemType.ToString();
        string assetPath = AssetDatabase.GetAssetPath(this);
        EditorApplication.delayCall += () =>
        {
            AssetDatabase.RenameAsset(assetPath, name);
            AssetDatabase.SaveAssets();
        };
    }
#endif
}