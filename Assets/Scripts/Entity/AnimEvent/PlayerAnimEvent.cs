using UnityEngine;

public class PlayerAnimEvent : AnimEvent
{
    private Player _player;

    protected override void Start()
    {
        base.Awake();
        _player = PlayerManager.Instance.player;
    }

    public void Attack1()
    {
        Collider2D[] cds = Physics2D.OverlapCircleAll(_player.AttackPoint, _player.attackRangeArray[0],
            LayerMask.NameToLayer("Enemy"));
        foreach (var cd in cds)
        {
            _player.playerStats.DoDamage(cd.GetComponent<Entity>().entityStats);
        }
    }

    public void Attack2()
    {
        Collider2D[] cds = Physics2D.OverlapCircleAll(_player.AttackPoint, _player.attackRangeArray[1],
            LayerMask.NameToLayer("Enemy"));
        foreach (var cd in cds)
        {
            _player.playerStats.DoDamage(cd.GetComponent<Entity>().entityStats);
        }
    }

    public void Attack3()
    {
        Collider2D[] cds = Physics2D.OverlapCircleAll(_player.AttackPoint, _player.attackRangeArray[2],
            LayerMask.NameToLayer("Enemy"));
        foreach (var cd in cds)
        {
            _player.playerStats.DoDamage(cd.GetComponent<Entity>().entityStats);
        }
    }
}