public class SkillManager : MonoSingleton<SkillManager>
{
    public Skill_Roll skillRoll;

    protected override void Awake()
    {
        base.Awake();
        skillRoll = GetComponent<Skill_Roll>();
    }
}