using LitJson;

public class GameDataManager : Singleton<GameDataManager>
{
    public PlayerDataModel playerDataModel;

    public override void Initialize()
    {
        base.Initialize();
    }

    public void ParsePlayerData(JsonData jsonData)
    {
        playerDataModel = new PlayerDataModel();
        playerDataModel.Parse(jsonData);
    }
}