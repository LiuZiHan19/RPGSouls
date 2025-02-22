using UnityEngine;

public class EnemyOrcAnimEvent : AnimEvent
{
    public LayerMask attackLayer;
    private EnemyOrc _enemyOrc;

    protected override void Awake()
    {
        base.Awake();
        _enemyOrc = GetComponentInParent<EnemyOrc>();
    }

    private void Attack()
    {
        Collider2D[] cds =
            Physics2D.OverlapCircleAll(_enemyOrc.attackPoint.position, _enemyOrc.attackRange, attackLayer);
        foreach (var cd in cds)
        {
            WarriorStats warriorStats = _enemyOrc.entityStats as WarriorStats;
            EntityStats stats = cd.GetComponent<Entity>().entityStats;
            switch (stats.statsType)
            {
                case E_CharacterStats.Almighty:
                    warriorStats.DoDamage(stats as AlmightyStats);
                    break;
                case E_CharacterStats.Mage:
                    warriorStats.DoDamage(stats as MageStats);
                    break;
                case E_CharacterStats.Warrior:
                    warriorStats.DoDamage(stats as WarriorStats);
                    break;
                default:
                    Debugger.Error(
                        $"Unknown CharacterStats: {stats.statsType}. Please check the input value and ensure it is one of the valid CharacterStats enum values.");
                    break;
            }
        }
    }
}