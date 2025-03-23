using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryEquipmentData", menuName = "Scriptable Object/Inventory/InventoryEquipmentData")]
public class InventoryEquipmentData : InventoryItemStatData
{
    public E_InventoryEquipment equipmentType;

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
        name = equipmentType.ToString();
        string assetPath = AssetDatabase.GetAssetPath(this);
        EditorApplication.delayCall += () =>
        {
            AssetDatabase.RenameAsset(assetPath, name);
            AssetDatabase.SaveAssets();
        };
    }
#endif
}