using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private static SkillManager m_instance;
    public static SkillManager Instance => m_instance;
    public Skill_Roll skillRoll;
    public Skill_Clone skillClone;
    public Skill_IdleBlock skillIdleBlock;

    private void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        skillRoll = GetComponent<Skill_Roll>();
        skillClone = GetComponent<Skill_Clone>();
        skillIdleBlock = GetComponent<Skill_IdleBlock>();

        GameDataManager.Instance.SkillDataModel.PareSelf();
    }
}