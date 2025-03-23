using UnityEngine;

public class EnemyGrimReaperDeathState : EnemyGrimReaperState
{
    public EnemyGrimReaperDeathState(StateMachine stateMachine, string animBoolName, Entity entity) : base(stateMachine,
        animBoolName, entity)
    {
    }

    public override void Update()
    {
        base.Update();
        if (grimReaper.IsTriggered())
        {
            GameObject.Destroy(grimReaper.gameObject);
        }
    }
}