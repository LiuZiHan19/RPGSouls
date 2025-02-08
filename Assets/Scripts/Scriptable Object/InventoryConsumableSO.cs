using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryEquipmentData", menuName = "Scriptable Object/Inventory/InventoryConsumableData")]
public class InventoryConsumableSO : InventoryItemBaseSO
{
    public E_InventoryConsumable consumableType;
    public int maxHealth;
    public int attackPower;
    public int armor;
    public int magicResistance;
    public int agility;
    public int intelligence;
    public int strength;
    public int vitality;
    public int criticalPower;
    public int criticalChance;
    public int evasion;
    public int lightPower;
    public int lightChance;
    public int chillPower;
    public int chillChance;
    public int ignitePower;
    public int igniteChance;

    private void OnValidate()
    {
#if UNITY_EDITOR
        name = consumableType.ToString();
        string assetPath = AssetDatabase.GetAssetPath(this);
        EditorApplication.delayCall += () =>
        {
            AssetDatabase.RenameAsset(assetPath, name);
            AssetDatabase.SaveAssets();
        };
#endif
    }
}