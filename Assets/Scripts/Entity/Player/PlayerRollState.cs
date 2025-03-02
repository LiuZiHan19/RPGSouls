using UnityEngine;

public class PlayerRollState : PlayerState
{
    public PlayerRollState(StateMachine stateMachine, string animBoolName, Entity entity) : base(stateMachine,
        animBoolName, entity)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(new Vector2(player.rollForce * player.facingDir, player.GetVelocity().y));
        player.skill.skillClone.Clone(player);
    }

    public override void Update()
    {
        base.Update();
        if (player.IsTriggered())
        {
            player.SetVelocity(Vector2.zero);
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        player.skill.skillClone.Clone(player);
        base.Exit();
    }
}