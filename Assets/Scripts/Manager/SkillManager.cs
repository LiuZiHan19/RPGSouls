public class SkillManager : MonoSingleton<SkillManager>
{
    public Skill_Roll skillRoll;
    public Skill_Clone skillClone;

    protected override void Awake()
    {
        base.Awake();
        skillRoll = GetComponent<Skill_Roll>();
        skillClone = GetComponent<Skill_Clone>();
    }
}