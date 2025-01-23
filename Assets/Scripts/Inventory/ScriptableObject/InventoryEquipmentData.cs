using UnityEngine;

[CreateAssetMenu(fileName = "InventoryEquipmentData", menuName = "Scriptable Object/Inventory/InventoryEquipmentData", order = 1)]
public class InventoryEquipmentData : InventoryItemBaseData
{
    public InventoryEquipmentType equipmentType;
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
}