using System;
using UnityEngine;

public class EnemyOrc : Enemy
{
    public EnemyOrcIdleState idleState;
    public EnemyOrcRunState runState;
    public EnemyOrcAttackState attackState;
    public EnemyOrcDeadState deathState;
    private Vector2 _moveVec;

    protected override void Awake()
    {
        base.Awake();
        idleState = new EnemyOrcIdleState(stateMachine, "Idle", this);
        runState = new EnemyOrcRunState(stateMachine, "Run", this);
        attackState = new EnemyOrcAttackState(stateMachine, "Attack", this);
        deathState = new EnemyOrcDeadState(stateMachine, "Dead", this);
        stateMachine.Initialise(idleState);
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deathState);
        foreach (var dropItem in dropItems)
        {
            Instantiate(dropItem, transform.position, Quaternion.identity);
        }
    }
}