using UnityEngine;

public class AnimEvent_Player : EntityAnimationEvent
{
    public LayerMask attackLayer;
    public Transform attackPoint;
    private Player _player;

    protected override void Start()
    {
        base.Awake();
        _player = PlayerManager.Instance.player;
    }

    public void Attack1()
    {
        Attack(0);
    }

    public void Attack2()
    {
        Attack(1);
    }

    public void Attack3()
    {
        Attack(2);
    }

    private void Attack(int rangeIndex)
    {
        // LayerMask.NameToLayer("Enemy") 检测不了 默认检测default层
        GameEventDispatcher.PlayerAttack?.Invoke(attackPoint);

        // 施加轻微攻击移动力
        _player.SetVelocity(new Vector2(_player.attackSlightForce[rangeIndex] * _player.facingDir,
            _player.Velocity.y));

        Collider2D[] cds =
            Physics2D.OverlapCircleAll(_player.attackPoint.position, _player.attackRangeArray[rangeIndex], attackLayer);
        foreach (var cd in cds)
        {
            // 施加击退力
            cd.GetComponent<Entity>().Knockback(_player.knockbackForce);

            PlayerStats playerStats = _player.playerStats;
            EntityStats stats = cd.GetComponent<Entity>().entityStats;
            switch (stats.statsType)
            {
                case CharacterStatsType.Almighty:
                    playerStats.DoDamage(stats as AlmightyStats);
                    break;
                case CharacterStatsType.Mage:
                    playerStats.DoDamage(stats as MageStats);
                    break;
                case CharacterStatsType.Warrior:
                    playerStats.DoDamage(stats as WarriorStats);
                    break;
                default:
                    Debugger.Error(
                        $"Unknown CharacterStats: {stats.statsType}. Please check the input value and ensure it is one of the valid CharacterStats enum values.");
                    break;
            }
        }
    }
}