using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "InventoryEquipmentData", menuName = "Scriptable Object/Inventory/InventoryItemData")]
public class InventoryItemData : InventoryItemBaseData
{
    [FormerlySerializedAs("itemType")] public InventoryItemID itemID;

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
        name = itemID.ToString();
        string assetPath = AssetDatabase.GetAssetPath(this);
        EditorApplication.delayCall += () =>
        {
            AssetDatabase.RenameAsset(assetPath, name);
            AssetDatabase.SaveAssets();
        };
    }
#endif
}