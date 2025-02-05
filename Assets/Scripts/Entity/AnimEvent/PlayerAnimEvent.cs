using UnityEngine;

public class PlayerAnimEvent : AnimEvent
{
    private Player _player;

    protected override void Awake()
    {
        base.Awake();
        _player = GetComponentInParent<Player>();
    }

    public void Attack1()
    {
        Physics2D.OverlapCircle(_player.transform.position, _player.attackRange);
    }

    public void Attack2()
    {
    }

    public void Attack3()
    {
    }
}