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
        EventSubscriber.PlayerAttack?.Invoke(attackPoint);

        // 施加轻微攻击移动力
        _player.SetVelocity(new Vector2(_player.attackSlightForce[rangeIndex] * _player.facingDir,
            _player.Velocity.y));

        Collider2D[] cds =
            Physics2D.OverlapCircleAll(_player.attackPoint.position, _player.attackRangeArray[rangeIndex], attackLayer);
        foreach (var cd in cds)
        {
            Entity target = cd.GetComponent<Entity>();
            if (target == null) continue;
            target.Knockback(_player.knockbackForce);
            _player.playerStats.DoDamage(target.entityStats);
        }
    }
}