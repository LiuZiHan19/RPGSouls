using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "Scriptable Object/Skill/SkillData")]
public class SkillData : ScriptableObject
{
    public SkillID SkillID;
    public int price;
    public SkillID[] unlockCondition;
}