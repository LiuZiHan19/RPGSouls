public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(StateMachine stateMachine, string animBoolName, Entity entity) : base(stateMachine,
        animBoolName, entity)
    {
    }

    public override void Update()
    {
        base.Update();
    }
}