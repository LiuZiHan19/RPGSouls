using UnityEngine;

public class Player : Entity
{
    private Rigidbody2D _rb;
    private StateMachine _stateMachine;
    private PlayerIdleState _idleState;

    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody2D>();
        _stateMachine = new StateMachine();
        _idleState = new PlayerIdleState(_stateMachine, PlayerStateConst.IdleState, this);
        _stateMachine.Initialise(_idleState);
    }

    protected override void Update()
    {
        base.Update();
        _stateMachine.currentState.Update();
    }
}