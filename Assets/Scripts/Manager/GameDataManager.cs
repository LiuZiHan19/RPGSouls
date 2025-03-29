using System;
using UnityEngine;
using UnityEngine.Events;

public class GameDataManager : MonoBehaviour
{
    private static GameDataManager instance;
    public static GameDataManager Instance => instance;

    public GameDataModel GameDataModel { get; set; } = new GameDataModel();
    public PlayerDataModel PlayerDataModel { get; set; } = new PlayerDataModel();
    public InventoryDataModel InventoryDataModel { get; set; } = new InventoryDataModel();
    public SkillDataModel SkillDataModel { get; set; } = new SkillDataModel();

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveGameData(UnityAction callback = null)
    {
        SavePlayerData(() =>
        {
            Debugger.Info(
                $"[GameDataManager] Save Player Data Successfully | Class: {GetType().Name}");
            SaveInventoryData(() =>
            {
                Debugger.Info(
                    $"[GameDataManager] Save Inventory Data Successfully | Class: {GetType().Name}");
                SaveSkillData(() =>
                {
                    Debugger.Info(
                        $"[GameDataManager] Save Skill Data Successfully | Class: {GetType().Name}");
                    callback?.Invoke();
                });
            });
        });

        GameDataModel.Save();
    }

    public void LoadGameData(UnityAction callback = null)
    {
        LoadPlayerData(() =>
        {
            Debugger.Info(
                $"[GameDataManager] Load Player Data Successfully | Class: {GetType().Name}");
            LoadInventoryData(() =>
            {
                Debugger.Info(
                    $"[GameDataManager] Load Inventory Data Successfully | Class: {GetType().Name}");
                LoadSkillData(() =>
                {
                    Debugger.Info(
                        $"[GameDataManager] Load Skill Data Successfully | Class: {GetType().Name}");
                    callback?.Invoke();
                });
            });
        });

        GameDataModel.Load();
    }

    public void LoadPlayerData(UnityAction callback = null)
    {
        JSONManager.Instance.LoadJsonDataAsync("PlayerData", jsonData =>
        {
            PlayerDataModel.jsonData = jsonData;
            callback?.Invoke();
        });
    }

    public void SavePlayerData(UnityAction callback = null)
    {
        JSONManager.Instance.SaveJsonDataAsync(PlayerDataModel.GetSaveJsonData().ToJson(), "PlayerData", callback);
    }

    public void LoadInventoryData(UnityAction callback = null)
    {
        JSONManager.Instance.LoadJsonDataAsync("InventoryData", jsonData =>
        {
            InventoryDataModel.jsonData = jsonData;
            callback?.Invoke();
        });
    }

    public void SaveInventoryData(UnityAction callback = null)
    {
        JSONManager.Instance.SaveJsonDataAsync(InventoryDataModel.GetSaveJsonData().ToJson(), "InventoryData",
            callback);
    }

    public void LoadSkillData(UnityAction callback = null)
    {
        JSONManager.Instance.LoadJsonDataAsync("SkillData", jsonData =>
        {
            SkillDataModel.jsonData = jsonData;
            callback?.Invoke();
        });
    }

    public void SaveSkillData(UnityAction callback = null)
    {
        JSONManager.Instance.SaveJsonDataAsync(SkillDataModel.GetSaveJsonData().ToJson(), "SkillData", callback);
    }

    public SkillData GetSkillDataByID(SkillID skillID)
    {
        foreach (var skillData in GameResources.Instance.SkillDataManifest.SkillDataList)
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