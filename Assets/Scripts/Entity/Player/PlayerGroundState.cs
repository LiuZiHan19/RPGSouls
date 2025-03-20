using UnityEngine;

public class PlayerGroundState : PlayerState
{
    public PlayerGroundState(StateMachine stateMachine, string animBoolName, Entity entity) : base(stateMachine,
        animBoolName, entity)
    {
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space) && player.IsGrounded())
        {
            stateMachine.ChangeState(player.jumpState);
        }

        if (Input.GetMouseButtonDown(0))
        {
            stateMachine.ChangeState(player.attackState);
        }

        if (Input.GetKeyDown(KeyCode.F) && player.IsGrounded() && player.skill.skillRoll.CanRelease())
        {
            stateMachine.ChangeState(player.rollState);
            isReturn = true;
        }

        if (Input.GetMouseButtonDown(1) && player.skill.skillIdleBlock.CanRelease())
        {
            stateMachine.ChangeState(player.idleBlockState);
        }
    }
}