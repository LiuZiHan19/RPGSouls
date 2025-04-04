using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class DataManager : MonoBehaviour
{
    private static DataManager instance;
    public static DataManager Instance => instance;

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

    public void LoadData(UnityAction callback = null)
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
        JSONManager.Instance.LoadJsonDataAsync("PlayerData", data =>
        {
            PlayerDataModel.SetJSONData(data);
            callback?.Invoke();
        });
    }

    public void SavePlayerData(UnityAction callback = null)
    {
        JSONManager.Instance.SaveJsonDataAsync(PlayerDataModel.GetJSONData().ToJson(), "PlayerData", callback);
    }

    public void LoadInventoryData(UnityAction callback = null)
    {
        JSONManager.Instance.LoadJsonDataAsync("InventoryData", data =>
        {
            InventoryDataModel.SetJSONData(data);
            callback?.Invoke();
        });
    }

    public void SaveInventoryData(UnityAction callback = null)
    {
        JSONManager.Instance.SaveJsonDataAsync(InventoryDataModel.GetJSONData().ToJson(), "InventoryData", callback);
    }

    public void LoadSkillData(UnityAction callback = null)
    {
        JSONManager.Instance.LoadJsonDataAsync("SkillData", data =>
        {
            SkillDataModel.SetJSONData(data);
            callback?.Invoke();
        });
    }

    public void SaveSkillData(UnityAction callback = null)
    {
        JSONManager.Instance.SaveJsonDataAsync(SkillDataModel.GetJSONData().ToJson(), "SkillData", callback);
    }

    public SkillData LoadSkillData(SkillID skillID)
    {
        foreach (var skillData in GameResources.Instance.SkillDataManifest.SkillDataList)
        {
            if (skillID == skillData.skillID)
            {
                return skillData;
            }
        }

        Debugger.Warning(
            $"[GameDataManager] SkillData not found in {nameof(LoadSkillData)} | Class: {GetType().Name}");
        return null;
    }

    public SkillData LoadSkillData(string guid)
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

    public AudioData LoadAudioData(AudioID audioID)
    {
        AudioData audioData = GameResources.Instance.AudioDataManifest.audioDataList.Find(x => x.audioID == audioID);
        if (audioData == null)
        {
            Debugger.Error($"AudioData not found for audioID: {audioID}. " +
                           $"Available audioIDs: {string.Join(", ", GameResources.Instance.AudioDataManifest.audioDataList.Select(x => x.audioID))}");
        }

        return audioData;
    }

    public InventoryItemBaseData LoadInventoryItemData(string guid)
    {
        List<InventoryItemBaseData> configurationData = GameResources.Instance.InventoryDataManifest.equipmentDataList;

        foreach (var itemData in configurationData)
        {
            if (guid == itemData.id)
            {
                return itemData;
            }
        }

        Debugger.Error($"[Inventory Load Data Error] 无法找到物品: {guid}");
        return null;
    }

    public EnemyData LoadEnemyData(EnemyID enemyID)
    {
        EnemyDataManifest enemyDataManifest = GameResources.Instance.EnemyDataManifest;
        EnemyData enemyData = enemyDataManifest.enemyDataList.Find(x => x.enemyID == enemyID);

        if (enemyData == null)
        {
            string availableIDs = string.Join(", ", enemyDataManifest.enemyDataList.Select(x => x.enemyID.ToString()));
            Debugger.Error($"EnemyData not found for enemyID: {enemyID}. Available enemyIDs: {availableIDs}");
            return null;
        }

        return enemyData;
    }
}