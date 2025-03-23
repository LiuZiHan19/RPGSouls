public class EnemyOrcAttackState : EnemyOrcState
{
    public EnemyOrcAttackState(StateMachine stateMachine, string animBoolName, Entity entity) : base(stateMachine,
        animBoolName, entity)
    {
    }

    public override void Update()
    {
        base.Update();
        if (enemyOrc.IsTriggered())
        {
            stateMachine.ChangeState(enemyOrc.IdleState);
        }
    }
}