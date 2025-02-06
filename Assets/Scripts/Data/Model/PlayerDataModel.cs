using LitJson;

public class PlayerDataModel : JsonModel
{
    public PlayerStats playerStats;

    public override void Parse(JsonData jsonData)
    {
        base.Parse(jsonData);
        if (jsonData.Keys.Contains("currentHealth") && jsonData["currentHealth"] != null)
        {
            int currentHealth = 0;
            int.TryParse(jsonData["currentHealth"].ToString(), out currentHealth);
            playerStats.currenHealth = currentHealth;
        }
    }

    public override JsonData GetJsonData()
    {
        return base.GetJsonData();
    }
}