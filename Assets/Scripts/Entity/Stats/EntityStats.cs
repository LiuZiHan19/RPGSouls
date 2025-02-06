using UnityEngine;
using Random = UnityEngine.Random;

public abstract class EntityStats : MonoBehaviour
{
    public CharacterStatsType statsType;
    public int currenHealth;
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
                currenHealth -= 5;
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
        currenHealth -= damage;
        currenHealth = Mathf.Clamp(currenHealth, 0, currenHealth);
    }

    public void SetMagicStatus(MagicStatus status)
    {
        switch (status)
        {
            case MagicStatus.Ignite:
                isIgnited = true;
                igniteTimer = 2.5f;
                break;
            case MagicStatus.Chill:
                isChilled = true;
                chillTimer = 2.5f;
                break;
            case MagicStatus.Lighting:
                isShocked = true;
                shockedTimer = 2.5f;
                break;
            default:
                Logger.Error(
                    $"Unknown MagicStatus: {status}. Please check the input value and ensure it is one of the valid MagicStatus enum values.");
                break;
        }
    }

    protected int GetRandomValue() => Random.Range(0, 101);
}