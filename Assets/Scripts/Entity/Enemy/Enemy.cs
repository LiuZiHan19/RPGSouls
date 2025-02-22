using System;
using UnityEngine;

public class Enemy : Entity
{
    public GameObject[] dropItems;
    public float attackRange;
    public float canAttackRange;
    public Transform attackPoint;
    private ColliderChecker _colliderChecker;

    protected override void Awake()
    {
        base.Awake();
        _colliderChecker = transform.Find("PlayerCheck").GetComponent<ColliderChecker>();
    }

    public bool CanAttack()
    {
        bool canAttack = Vector2.Distance(transform.position, PlayerManager.Instance.player.transform.position) <
                         canAttackRange;
        return canAttack;
    }

    public void CheckFlip()
    {
        if (PlayerManager.Instance.player.transform.position.x > transform.position.x && isFacingRight == false ||
            PlayerManager.Instance.player.transform.position.x < transform.position.x && isFacingRight == true)
        {
            Flip();
        }
    }

    public bool IsCheckedPlayer()
    {
        return _colliderChecker.IsChecked();
    }

    public void Move()
    {
        rb.velocity = new Vector2(facingDir * moveSpeed, rb.velocity.y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireSphere(attackPoint.position, canAttackRange);
    }
}