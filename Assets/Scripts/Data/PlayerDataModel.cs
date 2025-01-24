using LitJson;

public class PlayerDataModel : JsonModel
{
    public int currentHealth;
    public Stat maxHealth;
    public Stat attackPower;
    public Stat armor;
    public Stat magicResistance;
    public Stat agility;
    public Stat intelligence;
    public Stat strength;
    public Stat vitality;
    public Stat criticalPower;
    public Stat criticalChance;
    public Stat evasion;
    public Stat lightPower;
    public Stat lightChance;
    public Stat chillPower;
    public Stat chillChance;
    public Stat ignitePower;
    public Stat igniteChance;

    public override void Parse(JsonData jsonData)
    {
        base.Parse(jsonData);
        if (jsonData.Keys.Contains("currentHealth") && jsonData["currentHealth"] != null)
        {
            int currentHealth = 0;
            int.TryParse(jsonData["currentHealth"].ToString(), out currentHealth);
            this.currentHealth = currentHealth;
        }
    }

    public override JsonData GetJsonData()
    {
        jsonData["currentHealth"] = currentHealth;
        return base.GetJsonData();
    }
}