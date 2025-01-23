using UnityEngine;

[CreateAssetMenu(fileName = "InventoryEquipmentData", menuName = "Scriptable Object/Inventory/InventoryConsumableData", order = 2)]
public class InventoryConsumableData : InventoryItemBaseData
{
    public InventoryConsumableType consumableType;
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