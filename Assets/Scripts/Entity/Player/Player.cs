using UnityEngine;

public class Player : Entity
{
    public float jumpForce;
    public float attackRecoveryCooldown;
    public float[] attackRangeArray;
    public float[] attackSlightForce;
    public Transform attackPoint;
    public float rollForce;

    public PlayerIdleState IdleState { get; private set; }
    public PlayerRunState RunState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerFallState FallState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    public PlayerDeathState DeathState { get; private set; }
    public PlayerIdleBlockState IdleBlockState { get; private set; }
    public PlayerRollState RollState { get; private set; }

    public bool IsGrounded => _groundedChecker.IsChecked;
    public Vector2 Velocity => _rb.velocity;
    public Vector2 GetInput => _input;

    public SkillManager skill => SkillManager.Instance;
    public PlayerStats playerStats => entityStats as PlayerStats;

    private Rigidbody2D _rb;
    private ColliderChecker _groundedChecker;

    private int _attackCounter = 0;
    private float _attackTimer = 0;
    private Vector2 _input = new Vector2();
    private Vector2 _move = new Vector2();
    private Vector2 _jump = new Vector2();

    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody2D>();
        _groundedChecker = GetComponentInChildren<ColliderChecker>();

        IdleState = new PlayerIdleState(stateMachine, "Idle", this);
        RunState = new PlayerRunState(stateMachine, "Run", this);
        JumpState = new PlayerJumpState(stateMachine, "Jump", this);
        FallState = new PlayerFallState(stateMachine, "Fall", this);
        AttackState = new PlayerAttackState(stateMachine, "Attack", this);
        DeathState = new PlayerDeathState(stateMachine, "Death", this);
        IdleBlockState = new PlayerIdleBlockState(stateMachine, "IdleBlock", this);
        RollState = new PlayerRollState(stateMachine, "Roll", this);
        stateMachine.Initialise(IdleState);
    }

    protected override void Start()
    {
        base.Start();
        PlayerManager.Instance.Initialize();
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        _input.x = Input.GetAxisRaw("Horizontal");
        _input.y = Input.GetAxisRaw("Vertical");

        UpdateAttackTimer();
    }

    #region Attack

    private void UpdateAttackTimer()
    {
        _attackTimer -= Time.deltaTime;
        if (_attackTimer <= 0)
        {
            _attackCounter = 0;
        }
    }

    public void SetAttackAnim()
    {
        animator.SetInteger("AttackCounter", GetAttackCounter());
    }

    public int GetAttackCounter()
    {
        _attackTimer = attackRecoveryCooldown;
        _attackCounter++;
        if (_attackCounter > 3)
            _attackCounter = 1;
        return _attackCounter;
    }

    #endregion

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
        if (GetInput.x > 0 && isFacingRight == false)
        {
            Flip();
        }
        else if (GetInput.x < 0 && isFacingRight == true)
        {
            Flip();
        }
    }

    public void PlayAttackSfx()
    {
        switch (_attackCounter)
        {
            case 1:
                SoundManager.Instance.PlaySfx("Sound/sfx_attack1");
                break;
            case 2:
                SoundManager.Instance.PlaySfx("Sound/sfx_attack2");
                break;
            case 3:
                SoundManager.Instance.PlaySfx("Sound/sfx_attack3");
                break;
        }
    }

    public override void Die()
    {
        base.Die();
        SoundManager.Instance.PlaySfx("Sound/sfx_death");
        stateMachine.ChangeState(DeathState);
        GameEventDispatcher.OnPlayerDead?.Invoke();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRangeArray[0]);
        Gizmos.DrawWireSphere(attackPoint.position, attackRangeArray[1]);
        Gizmos.DrawWireSphere(attackPoint.position, attackRangeArray[2]);
    }
}