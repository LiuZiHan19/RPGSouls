using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public abstract class EntityStats : MonoBehaviour
{
    public E_CharacterStats statsType;
    [FormerlySerializedAs("currenHealth")] public int currentHealth;
    public Stat maxHealth;
    public Stat attackPower;
    public Stat magicPower;
    public Stat armor;
    public Stat magicResistance;
    public bool isChilled; // reduce armor by 20%
    public bool isIgnited; // does damage over time
    public bool isShocked; // reduce magicResistance by 20%
    private float chillTimer;
    private float igniteTimer;
    private float shockedTimer;
    private float burnTimer;
    private Entity entity;

    protected void Awake()
    {
        entity = GetComponent<Entity>();
        currentHealth = maxHealth.GetValue();
    }

    protected virtual void Update()
    {
        ChillLogic();
        IgniteLogic();
    }

    private void ChillLogic()
    {
        if (isChilled)
        {
            chillTimer -= Time.deltaTime;
            if (chillTimer < 0)
            {
                isChilled = false;
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
                isIgnited = false;
            }
        }
    }

    public virtual void DoDamage(EntityStats target)
    {
        if (target is AlmightyStats almightyStats)
        {
            if (almightyStats.CanEvasion()) return;
        }
        else if (target is MageStats mageStats)
        {
            if (mageStats.CanEvasion()) return;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, currentHealth);
        if (currentHealth == 0) entity.Die();
    }

    public void SetMagicStatus(E_MagicStatus status)
    {
        switch (status)
        {
            case E_MagicStatus.Ignite:
                isIgnited = true;
                igniteTimer = 2.5f;
                break;
            case E_MagicStatus.Chill:
                isChilled = true;
                chillTimer = 2.5f;
                break;
            case E_MagicStatus.Lighting:
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
}