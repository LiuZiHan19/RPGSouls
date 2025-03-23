using UnityEngine;

public class EntityAnimationEvent : MonoBehaviour
{
    public Entity entity;

    protected virtual void Awake()
    {
        entity = GetComponentInParent<Entity>();
    }

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
    }

    public void Trigger()
    {
        entity.stateMachine.currentState.triggered = true;
    }

    public bool IsTriggered()
    {
        return entity.stateMachine.currentState.triggered;
    }
}