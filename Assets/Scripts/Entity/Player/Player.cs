using UnityEngine;

public class Player : Entity
{
    public float jumpForce;
    public float attackRecoveryCooldown;
    public float[] attackRangeArray;
    public float[] attackSlightForce;
    public Transform attackPoint;
    public float rollForce;

    public PlayerIdleState idleState;
    public PlayerRunState runState;
    public PlayerJumpState jumpState;
    public PlayerFallState fallState;
    public PlayerAttackState attackState;
    public PlayerDeathState deathState;
    public PlayerIdleBlockState idleBlockState;
    public PlayerRollState rollState;

    public SkillManager skill => SkillManager.Instance;
    public PlayerStats playerStats;

    private Rigidbody2D _rb;
    private ColliderChecker _colliderChecker;

    private int _attackCounter;
    private float _attackTimer;
    private Vector2 _input = new Vector2();
    private Vector2 _move = new Vector2();
    private Vector2 _jump = new Vector2();

    protected override void Awake()
    {
        base.Awake();
        PlayerManager.Instance.Initialize();
        playerStats = entityStats as PlayerStats;
        _rb = GetComponent<Rigidbody2D>();
        _colliderChecker = GetComponentInChildren<ColliderChecker>();

        idleState = new PlayerIdleState(stateMachine, "Idle", this);
        runState = new PlayerRunState(stateMachine, "Run", this);
        jumpState = new PlayerJumpState(stateMachine, "Jump", this);
        fallState = new PlayerFallState(stateMachine, "Fall", this);
        attackState = new PlayerAttackState(stateMachine, "Attack", this);
        deathState = new PlayerDeathState(stateMachine, "Death", this);
        idleBlockState = new PlayerIdleBlockState(stateMachine, "IdleBlock", this);
        rollState = new PlayerRollState(stateMachine, "Roll", this);
        stateMachine.Initialise(idleState);
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

    public bool IsGrounded()
    {
        return _colliderChecker.IsChecked();
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
        stateMachine.ChangeState(deathState);
        EventDispatcher.OnPlayerDead?.Invoke();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRangeArray[0]);
        Gizmos.DrawWireSphere(attackPoint.position, attackRangeArray[1]);
        Gizmos.DrawWireSphere(attackPoint.position, attackRangeArray[2]);
    }
}