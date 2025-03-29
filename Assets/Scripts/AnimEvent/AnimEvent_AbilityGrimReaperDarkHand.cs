using UnityEngine;

public class AnimEvent_AbilityGrimReaperDarkHand : AnimationEvent
{
    private Ability_GrimReaperDarkHand m_abilityGrimReaper;

    protected void Awake()
    {
        m_abilityGrimReaper = GetComponentInParent<Ability_GrimReaperDarkHand>();
    }

    protected void Update()
    {
        if (IsTriggered)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    public void Attack()
    {
        Collider2D[] cds =
            Physics2D.OverlapCircleAll(m_abilityGrimReaper.AttackPosition, m_abilityGrimReaper.attackRadius);
        foreach (Collider2D cd in cds)
        {
            if (cd.CompareTag("player"))
            {
                GameManager.Instance.GrimReaper.entityStats.DoDamage(PlayerManager.Instance.player.playerStats);
            }
        }
    }
}