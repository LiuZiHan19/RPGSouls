using LitJson;

public abstract class JsonModel
{
    public JsonData jsonData;

    public virtual void Parse(JsonData jsonData) => this.jsonData = jsonData;

    public virtual JsonData GetJsonData() => jsonData;

    private int GetInteger(JsonData jsonData, string key, int defaultValue = 0)
    {
        if (jsonData.Keys.Contains(key) && jsonData[key] != null)
        {
            int value = 0;
            int.TryParse(jsonData[key].ToString(), out value);
            return value;
        }

        Logger.Warning($"Missing or invalid key: '{key}' in JSON data. Returning default value: {defaultValue}. " +
                       $"Ensure that the key exists and is a valid integer.");
        return defaultValue;
    }

    private string GetString(JsonData jsonData, string key, string defaultValue = "")
    {
        if (jsonData.Keys.Contains(key) && jsonData[key] != null)
        {
            return jsonData[key].ToString();
        }

        Logger.Warning($"Missing or invalid key: '{key}' in JSON data. Returning default value: {defaultValue}. " +
                       $"Ensure that the key exists and is a valid string.");
        return defaultValue;
    }
}