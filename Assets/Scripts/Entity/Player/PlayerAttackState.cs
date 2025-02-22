public class PlayerAttackState : PlayerState
{
    public PlayerAttackState(StateMachine stateMachine, string animBoolName, Entity entity) : base(stateMachine,
        animBoolName, entity)
    {
    }

    public override void Enter()
    {
        player.SetAttackAnim();
        player.PlayAttackSfx();
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        if (player.IsTriggered())
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}