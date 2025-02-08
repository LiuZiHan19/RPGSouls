public class State
{
    protected StateMachine stateMachine;
    protected string animBoolName;
    protected Entity entity;

    public State(StateMachine stateMachine, string animBoolName, Entity entity)
    {
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
        this.entity = entity;
    }

    public virtual void Enter()
    {
        Logger.Info($"{entity.entityName} Enter {animBoolName} State");
        entity.animator.SetBool(animBoolName, true);
    }

    public virtual void Update()
    {
    }

    public virtual void Exit()
    {
        Logger.Info($"{entity.entityName} Exit {animBoolName} State");
        entity.animator.SetBool(animBoolName, false);
    }
}