using LitJson;
using UnityEngine;
using UnityEngine.Events;

public class GameDataManager : MonoSingletonDontDes<GameDataManager>
{
    public SkillDataManifest SkillDataManifest;
    public InventoryDataManifest InventoryDataManifest;
    public GameDataModel GameDataModel { get; set; } = new GameDataModel();
    public PlayerDataModel PlayerDataModel { get; set; } = new PlayerDataModel();
    public InventoryDataModel InventoryDataModel { get; set; } = new InventoryDataModel();
    public SkillDataModel SkillDataModel { get; set; } = new SkillDataModel();

    protected override void Awake()
    {
        base.Awake();
        LoadPlayerData();
        LoadInventoryData();
        LoadSkillData();
    }

    public void LoadPlayerData(UnityAction callback = null)
    {
        JSONManager.Instance.LoadJsonDataAsync("PlayerData", jsonData => { PlayerDataModel.jsonData = jsonData; });
    }

    public void SavePlayerData(UnityAction callback = null)
    {
        JSONManager.Instance.SaveJsonDataAsync(PlayerDataModel.GetSaveJsonData().ToJson(), "PlayerData", callback);
    }

    public void LoadInventoryData(UnityAction callback = null)
    {
        JSONManager.Instance.LoadJsonDataAsync("InventoryData",
            jsonData => { InventoryDataModel.jsonData = jsonData; });
    }

    public void SaveInventoryData(UnityAction callback = null)
    {
        JSONManager.Instance.SaveJsonDataAsync(InventoryDataModel.GetSaveJsonData().ToJson(), "InventoryData",
            callback);
    }

    public void LoadSkillData(UnityAction callback = null)
    {
        JSONManager.Instance.LoadJsonDataAsync("SkillData", jsonData => { SkillDataModel.jsonData = jsonData; });
    }

    public void SaveSkillData(UnityAction callback = null)
    {
        JSONManager.Instance.SaveJsonDataAsync(SkillDataModel.GetSaveJsonData().ToJson(), "SkillData", callback);
    }

    public SkillData GetSkillDataByID(SkillID skillID)
    {
        foreach (var skillData in SkillDataManifest.SkillDataList)
        {
            if (skillID == skillData.SkillID)
            {
                return skillData;
            }
        }

        Debugger.Warning(
            $"[GameDataManager] SkillData not found in {nameof(GetSkillDataByID)} | Class: {GetType().Name}");
        return null;
    }

    public bool CanUnlockSkill(SkillID skillID)
    {
        SkillData skillData = GetSkillDataByID(skillID);
        SkillID[] skillIds = skillData.unlockCondition;
        foreach (var skillId in skillIds)
        {
            switch (skillId)
            {
                case SkillID.Roll:
                    if (SkillDataModel.skillRollData.isUlocked == false)
                    {
                        return false;
                    }

                    break;
                case SkillID.IdleBlock:
                    if (SkillDataModel.skillIdleBlockData.isUlocked == false)
                    {
                        return false;
                    }

                    break;
                case SkillID.Clone:
                    if (SkillDataModel.skillCloneData.isUlocked == false)
                    {
                        return false;
                    }

                    break;
            }
        }

        return true;
    }
}