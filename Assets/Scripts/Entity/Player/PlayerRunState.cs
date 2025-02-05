public class PlayerRunState : PlayerGroundState
{
    public PlayerRunState(StateMachine stateMachine, string animBoolName, Entity entity) : base(stateMachine,
        animBoolName, entity)
    {
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(player.GetMove());
        player.CheckFlip();
        if (player.GetInput().x == 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}