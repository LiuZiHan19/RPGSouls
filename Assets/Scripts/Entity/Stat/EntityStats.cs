using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public abstract class EntityStats : MonoBehaviour
{
    public UnityAction<float> takeDamageCallback;
    public E_CharacterStats statsType;
    public int currentHealth;
    public Stat maxHealth;
    public Stat attackPower;
    public Stat magicPower;
    public Stat armor;
    public Stat magicResistance;
    public bool isChilled; // reduce armor by 20%
    public bool isIgnited; // does damage over time
    public bool isShocked; // reduce magicResistance by 20%
    public bool isInvincible;
    public bool isDead;

    private float chillTimer;
    private float igniteTimer;
    private float shockedTimer;
    private float burnTimer;
    private Entity entity;

    protected virtual void Awake()
    {
        entity = GetComponent<Entity>();
        currentHealth = maxHealth.GetValue();
    }

    protected virtual void Update()
    {
        ChillLogic();
        IgniteLogic();
        LightingLogic();
    }

    private void ChillLogic()
    {
        if (isChilled)
        {
            chillTimer -= Time.deltaTime;
            if (chillTimer < 0)
            {
                entity.entityFX.StopMagicStatusFX();
                isChilled = false;
            }
        }
    }

    private void LightingLogic()
    {
        if (isShocked)
        {
            shockedTimer -= Time.deltaTime;
            if (shockedTimer < 0)
            {
                entity.entityFX.StopMagicStatusFX();
                isShocked = false;
            }
        }
    }

    private void IgniteLogic()
    {
        if (isIgnited)
        {
            igniteTimer -= Time.deltaTime;
            burnTimer -= Time.deltaTime;
            if (burnTimer < 0)
            {
                burnTimer = 0.25f;
                currentHealth -= 5;
            }

            if (igniteTimer < 0)
            {
                entity.entityFX.StopMagicStatusFX();
                isIgnited = false;
            }
        }
    }

    public virtual void DoDamage(EntityStats target)
    {
    }

    public virtual void TakeDamage(int damage)
    {
        if (isDead) return;
        if (isInvincible) return;
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
            entity.Die();
        }

        takeDamageCallback?.Invoke((float)currentHealth / maxHealth.GetValue());
    }

    public void SetMagicStatus(E_MagicStatus status)
    {
        switch (status)
        {
            case E_MagicStatus.Ignite:
                entity.entityFX.PlayMagicStatusFX(E_MagicStatus.Ignite);
                isIgnited = true;
                igniteTimer = 2.5f;
                break;
            case E_MagicStatus.Chill:
                entity.entityFX.PlayMagicStatusFX(E_MagicStatus.Chill);
                isChilled = true;
                chillTimer = 2.5f;
                break;
            case E_MagicStatus.Lighting:
                entity.entityFX.PlayMagicStatusFX(E_MagicStatus.Lighting);
                isShocked = true;
                shockedTimer = 2.5f;
                break;
            default:
                Debugger.Error(
                    $"Unknown MagicStatus: {status}. Please check the input value and ensure it is one of the valid MagicStatus enum values.");
                break;
        }
    }

    protected int GetRandomValue() => Random.Range(0, 101);

    protected bool CanEvasion(EntityStats target)
    {
        if (target is AlmightyStats almightyStats)
        {
            if (almightyStats.CanEvasion()) return true;
        }
        else if (target is MageStats mageStats)
        {
            if (mageStats.CanEvasion()) return true;
        }

        return false;
    }
}