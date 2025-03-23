using UnityEngine;

public class AbilityGrimReaper : Ability
{
    public float attackRadius;
    public Vector2 AttackPosition => transform.position + Vector3.down;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + Vector3.down, attackRadius);
    }
}