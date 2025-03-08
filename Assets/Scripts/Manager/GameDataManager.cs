using UnityEngine.Events;

public class GameDataManager : Singleton<GameDataManager>
{
    public GameDataModel gameDataModel = new GameDataModel();
    public PlayerDataModel playerDataModel = new PlayerDataModel();
    public InventoryDataModel inventoryDataModel = new InventoryDataModel();
    public SkillDataModel skillDataModel = new SkillDataModel();

    public void LoadPlayerData(UnityAction callback = null)
    {
        JsonManager.Instance.LoadJsonDataAsync("PlayerData", jsonData => { playerDataModel.jsonData = jsonData; });
    }

    public void SavePlayerData(UnityAction callback = null)
    {
        JsonManager.Instance.SaveJsonDataAsync(playerDataModel.GetSaveJsonData().ToJson(), "PlayerData", callback);
    }

    public void LoadInventoryData(UnityAction callback = null)
    {
        JsonManager.Instance.LoadJsonDataAsync("InventoryData",
            jsonData => { inventoryDataModel.jsonData = jsonData; });
    }

    public void SaveInventoryData(UnityAction callback = null)
    {
        JsonManager.Instance.SaveJsonDataAsync(inventoryDataModel.GetSaveJsonData().ToJson(), "InventoryData",
            callback);
    }

    public void LoadSkillData()
    {
    }

    public void SaveSkillData()
    {
    }

    public void LoadGameData()
    {
    }

    public void SaveGameData()
    {
    }
}