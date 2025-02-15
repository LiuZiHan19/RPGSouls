public class EnemyOrcIdleState : EnemyOrcState
{
    public EnemyOrcIdleState(StateMachine stateMachine, string animBoolName, Entity entity) : base(stateMachine,
        animBoolName, entity)
    {
    }

    public override void Update()
    {
        base.Update();
        if (enemyOrc.IsCheckedPlayer())
        {
            enemyOrc.CheckFlip();
            if (enemyOrc.CanAttack())
            {
                stateMachine.ChangeState(enemyOrc.attackState);
            }
            else
            {
                stateMachine.ChangeState(enemyOrc.runState);
            }
        }
    }
}