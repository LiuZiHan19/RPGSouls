using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillDataManifest", menuName = "Scriptable Object/Skill/SkillDataManifest")]
public class SkillDataManifest : ScriptableObject
{
    public List<SkillData> SkillDataList;
}