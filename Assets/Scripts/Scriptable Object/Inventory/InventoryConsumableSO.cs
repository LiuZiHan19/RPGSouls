using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryEquipmentData", menuName = "Scriptable Object/Inventory/InventoryConsumableData")]
public class InventoryConsumableSO : InventoryItemStatSO
{
    public E_InventoryConsumable consumableType;

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
        name = consumableType.ToString();
        string assetPath = AssetDatabase.GetAssetPath(this);
        EditorApplication.delayCall += () =>
        {
            AssetDatabase.RenameAsset(assetPath, name);
            AssetDatabase.SaveAssets();
        };
    }
#endif
}