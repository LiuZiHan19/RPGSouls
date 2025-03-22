using UnityEngine;

public class EnemyOrc : Enemy
{
    public EnemyOrcIdleState idleState;
    public EnemyOrcBattleState BattleState;
    public EnemyOrcAttackState attackState;
    public EnemyOrcDeadState deathState;
    public EnemyOrcPatrolState patrolState;
    private Vector2 _moveVec;

    protected override void Awake()
    {
        base.Awake();
        idleState = new EnemyOrcIdleState(stateMachine, "Idle", this);
        BattleState = new EnemyOrcBattleState(stateMachine, "Run", this);
        attackState = new EnemyOrcAttackState(stateMachine, "Attack", this);
        deathState = new EnemyOrcDeadState(stateMachine, "Dead", this);
        patrolState = new EnemyOrcPatrolState(stateMachine, "Run", this);
        stateMachine.Initialise(patrolState);
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