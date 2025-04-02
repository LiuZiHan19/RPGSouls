using System;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private static SkillManager m_instance;
    public static SkillManager Instance => m_instance;
    public Skill_Roll SkillRoll { get; set; }
    public Skill_Clone SkillClone { get; set; }
    public Skill_IdleBlock SkillIdleBlock { get; set; }
    public Skill_MagicOrb SkillMagicOrb { get; set; }

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

        SkillRoll = GetComponent<Skill_Roll>();
        SkillClone = GetComponent<Skill_Clone>();
        SkillIdleBlock = GetComponent<Skill_IdleBlock>();
        SkillMagicOrb = GetComponent<Skill_MagicOrb>();

         DataManager.Instance.SkillDataModel.ParseJSONData(UpdateOnParseDataCompleted);
    }
 
    public bool GetSkillIsUnlocked(SkillID id)
    {
        Skill skill;
        switch (id)
        {
            case SkillID.Roll:
                skill = SkillRoll;
                break;
            case SkillID.IdleBlock:
                skill = SkillIdleBlock;
                break;
            case SkillID.Clone:
                skill = SkillClone;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(id), id, null);
        }

        return skill.isUnlocked;
    }

    public Skill GetSKill(SkillID id)
    {
        Skill skill;
        switch (id)
        {
            case SkillID.Roll:
                skill = SkillRoll;
                break;
            case SkillID.IdleBlock:
                skill = SkillIdleBlock;
                break;
            case SkillID.Clone:
                skill = SkillClone;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(id), id, null);
        }

        return skill;
    }

    public SkillData GetSkillData(SkillID skillID)
    {
        foreach (var skillData in GameResources.Instance.SkillDataManifest.SkillDataList)
        {
            if (skillID == skillData.skillID)
            {
                return skillData;
            }
        }

        Debugger.Warning(
            $"[GameDataManager] SkillData not found in {nameof(GetSkillData)} | Class: {GetType().Name}");
        return null;
    }

    private SkillData GetSkillData(string guid)
    {
        var skillData = GameResources.Instance.SkillDataManifest.SkillDataList;
        for (int i = 0; i < skillData.Count; i++)
        {
            if (guid == skillData[i].id)
            {
                return skillData[i];
            }
        }

        return null;
    }

    public bool CanUnlockSkill(SkillID skillID)
    {
        SkillData skillData = GetSkillData(skillID);
        SkillID[] skillIds = skillData.unlockCondition;
        foreach (var skillId in skillIds)
        {
            switch (skillId)
            {
                case SkillID.Roll:
                    if (SkillRoll.isUnlocked == false)
                    {
                        return false;
                    }

                    break;
                case SkillID.IdleBlock:
                    if (SkillIdleBlock.isUnlocked == false)
                    {
                        return false;
                    }

                    break;
                case SkillID.Clone:
                    if (SkillClone.isUnlocked == false)
                    {
                        return false;
                    }

                    break;
            }
        }

        return true;
    }

    private void UpdateOnParseDataCompleted()
    {
        SkillDataModel skillDataModel = DataManager.Instance.SkillDataModel;
        var skillItemDataList = skillDataModel.itemDataList;
        for (int i = 0; i < skillItemDataList.Count; i++)
        {
            var itemData = skillItemDataList[i];
            var id = GetSkillData(itemData.id).skillID;
            GetSKill(id).isUnlocked = itemData.isUnlocked;
        }
    }
}