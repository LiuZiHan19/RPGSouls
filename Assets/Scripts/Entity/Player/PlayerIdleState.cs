using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(StateMachine stateMachine, string animBoolName, Entity entity) : base(stateMachine,
        animBoolName, entity)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(Vector2.zero);
    }

    public override void Update()
    {
        if (player.GetVelocity().x != 0)
        {
            player.SetVelocity(new Vector2(0, player.GetVelocity().y));
        }
        
        base.Update();

        if (player.GetInput().x != 0)
        {
            stateMachine.ChangeState(player.runState);
        }
    }
}