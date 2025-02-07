using UnityEngine;

public class Player : Entity
{
    public float jumpForce;
    public float attackRecoveryCooldown;
    public float[] attackRangeArray;

    public PlayerIdleState idleState;
    public PlayerRunState runState;
    public PlayerJumpState jumpState;
    public PlayerFallState fallState;
    public PlayerAttackState attackState;
    public PlayerDeathState deathState;
    public PlayerIdleBlockState idleBlockState;
    public PlayerStats playerStats;
    public SkillManager skill;

    private Rigidbody2D _rb;
    private StateMachine _stateMachine;
    private EntityChecker _entityChecker;
    private AnimEvent _animEvent;

    private int _attackCounter;
    private float _attackTimer;
    private Vector2 _input = new Vector2();
    private Vector2 _move = new Vector2();
    private Vector2 _jump = new Vector2();
    private Vector2 _attackPoint;
    public Vector2 AttackPoint => _attackPoint;

    protected override void Awake()
    {
        base.Awake();
        playerStats = entityStats as PlayerStats;
        skill = SkillManager.Instance;
        _rb = GetComponent<Rigidbody2D>();
        _entityChecker = GetComponentInChildren<EntityChecker>();
        _animEvent = GetComponentInChildren<AnimEvent>();
        _attackPoint = transform.Find("AttackCheck").position;

        _stateMachine = new StateMachine();
        idleState = new PlayerIdleState(_stateMachine, PlayerStateConst.IdleState, this);
        runState = new PlayerRunState(_stateMachine, PlayerStateConst.RunState, this);
        jumpState = new PlayerJumpState(_stateMachine, PlayerStateConst.JumpState, this);
        fallState = new PlayerFallState(_stateMachine, PlayerStateConst.FallState, this);
        attackState = new PlayerAttackState(_stateMachine, PlayerStateConst.AttackState, this);
        deathState = new PlayerDeathState(_stateMachine, PlayerStateConst.DeathState, this);
        idleBlockState = new PlayerIdleBlockState(_stateMachine, PlayerStateConst.IdleBlockState, this);
        _stateMachine.Initialise(idleState);
    }

    protected override void Update()
    {
        base.Update();
        _stateMachine.currentState.Update();
        _input.x = Input.GetAxisRaw("Horizontal");
        _input.y = Input.GetAxisRaw("Vertical");

        UpdateAttackTimer();
    }

    private void UpdateAttackTimer()
    {
        _attackTimer -= Time.deltaTime;
        if (_attackTimer <= 0)
        {
            _attackCounter = 0;
        }
    }

    public int GetAttackCounter()
    {
        _attackTimer = attackRecoveryCooldown;
        _attackCounter++;
        if (_attackCounter > 3)
            _attackCounter = 1;
        return _attackCounter;
    }

    public bool IsGrounded()
    {
        return _entityChecker.IsColliding();
    }

    public bool IsTriggered()
    {
        return _animEvent.IsTriggered();
    }

    public Vector2 GetVelocity()
    {
        return _rb.velocity;
    }

    public Vector2 GetInput()
    {
        return _input;
    }

    public Vector2 GetMove()
    {
        _move.x = _input.x * moveSpeed;
        _move.y = _rb.velocity.y;
        return _move;
    }

    public Vector2 GetJump()
    {
        _jump.x = _rb.velocity.x;
        _jump.y = jumpForce;
        return _jump;
    }

    public void SetVelocity(Vector2 velocity)
    {
        _rb.velocity = velocity;
    }

    public void CheckFlip()
    {
        if (GetInput().x > 0 && isFacingRight == false)
        {
            Flip();
        }
        else if (GetInput().x < 0 && isFacingRight == true)
        {
            Flip();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        facingDir = -facingDir;
        transform.Rotate(0, 180, 0);
    }
}