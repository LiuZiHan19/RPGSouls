using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public int price;
    public bool isUnlocked;
    public float cooldown;
    public float cooldownTimer;

    protected virtual void Update()
    {
        cooldownTimer += Time.deltaTime;
    }

    public bool CanRelease()
    {
        if (cooldownTimer > cooldown)
        {
            cooldownTimer = 0;
            return true;
        }

        return false;
    }
}