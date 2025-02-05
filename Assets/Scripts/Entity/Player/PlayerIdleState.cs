public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(StateMachine stateMachine, string animBoolName, Entity entity) : base(stateMachine,
        animBoolName, entity)
    {
    }

    public override void Update()
    {
        base.Update();
        if (player.GetInput().x != 0)
        {
            stateMachine.ChangeState(player.runState);
        }
    }
}