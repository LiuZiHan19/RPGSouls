public class EnemyOrcRunState : EnemyOrcState
{
    public EnemyOrcRunState(StateMachine stateMachine, string animBoolName, Entity entity) : base(stateMachine,
        animBoolName, entity)
    {
    }

    public override void Update()
    {
        base.Update();
        enemyOrc.Move();
        if (enemyOrc.CanAttack())
        {
            stateMachine.ChangeState(enemyOrc.attackState);
        }
        else if (enemyOrc.IsCheckedPlayer() == false)
        {
            stateMachine.ChangeState(enemyOrc.idleState);
        }
    }
}