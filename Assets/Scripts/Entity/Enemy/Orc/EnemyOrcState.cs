public class EnemyOrcState : State
{
    protected EnemyOrc EnemyOrc;

    public EnemyOrcState(StateMachine stateMachine, string animBoolName, Entity entity) : base(stateMachine,
        animBoolName, entity)
    {
        EnemyOrc = entity as EnemyOrc;
    }
}