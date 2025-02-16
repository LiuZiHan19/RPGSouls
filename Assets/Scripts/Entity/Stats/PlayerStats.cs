using System;

public class PlayerStats : AlmightyStats, IDisposable
{
    protected override void Awake()
    {
        base.Awake();
        EventDispatcher.Equip += Equip;
        EventDispatcher.UnEquip += UnEquip;
    }

    private void Equip(InventoryItemBaseSO itemSO)
    {
        if (itemSO is InventoryItemStatSO statSO)
        {
            if (statSO.maxHealth > 0) maxHealth.AddModifier(statSO.maxHealth);
            if (statSO.attackPower > 0) attackPower.AddModifier(statSO.attackPower);
            if (statSO.armor > 0) armor.AddModifier(statSO.armor);
            if (statSO.magicResistance > 0) magicResistance.AddModifier(statSO.magicResistance);
            if (statSO.agility > 0) agility.AddModifier(statSO.agility);
            if (statSO.intelligence > 0) intelligence.AddModifier(statSO.intelligence);
            if (statSO.strength > 0) strength.AddModifier(statSO.strength);
            if (statSO.vitality > 0) vitality.AddModifier(statSO.vitality);
            if (statSO.criticalPower > 0) criticalPower.AddModifier(statSO.criticalPower);
            if (statSO.criticalChance > 0) criticalChance.AddModifier(statSO.criticalChance);
            if (statSO.evasion > 0) evasion.AddModifier(statSO.evasion);
            if (statSO.lighting > 0) lighting.AddModifier(statSO.lighting);
            if (statSO.chill > 0) chill.AddModifier(statSO.chill);
            if (statSO.ignite > 0) ignite.AddModifier(statSO.ignite);
        }
    }

    private void UnEquip(InventoryItemBaseSO itemSO)
    {
        if (itemSO is InventoryItemStatSO statSO)
        {
            if (statSO.maxHealth > 0) maxHealth.RemoveModifier(statSO.maxHealth);
            if (statSO.attackPower > 0) attackPower.RemoveModifier(statSO.attackPower);
            if (statSO.armor > 0) armor.RemoveModifier(statSO.armor);
            if (statSO.magicResistance > 0) magicResistance.RemoveModifier(statSO.magicResistance);
            if (statSO.agility > 0) agility.RemoveModifier(statSO.agility);
            if (statSO.intelligence > 0) intelligence.RemoveModifier(statSO.intelligence);
            if (statSO.strength > 0) strength.RemoveModifier(statSO.strength);
            if (statSO.vitality > 0) vitality.RemoveModifier(statSO.vitality);
            if (statSO.criticalPower > 0) criticalPower.RemoveModifier(statSO.criticalPower);
            if (statSO.criticalChance > 0) criticalChance.RemoveModifier(statSO.criticalChance);
            if (statSO.evasion > 0) evasion.RemoveModifier(statSO.evasion);
            if (statSO.lighting > 0) lighting.RemoveModifier(statSO.lighting);
            if (statSO.chill > 0) chill.RemoveModifier(statSO.chill);
            if (statSO.ignite > 0) ignite.RemoveModifier(statSO.ignite);
        }
    }

    public void Dispose()
    {
        EventDispatcher.Equip -= Equip;
        EventDispatcher.UnEquip -= UnEquip;
    }
}