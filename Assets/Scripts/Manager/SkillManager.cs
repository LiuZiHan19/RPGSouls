public class SkillManager : MonoSingleton<SkillManager>
{
    public Skill_Roll skillRoll;
    public Skill_Clone skillClone;
    public Skill_IdleBlock skillIdleBlock;

    protected override void Awake()
    {
        base.Awake();
        skillRoll = GetComponent<Skill_Roll>();
        skillClone = GetComponent<Skill_Clone>();
        skillIdleBlock = GetComponent<Skill_IdleBlock>();

        GameDataManager.Instance.SkillDataModel.PareSelf();
    }
}