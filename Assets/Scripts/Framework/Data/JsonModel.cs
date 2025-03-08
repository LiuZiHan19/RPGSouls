using LitJson;
using UnityEngine.Events;

public abstract class JsonModel
{
    public JsonData jsonData;

    public void PareSelf()
    {
        ParseData(jsonData);
    }

    public virtual void ParseData(JsonData jsonData, UnityAction callback = null) => this.jsonData = jsonData;

    public virtual JsonData GetSaveJsonData()
    {
        return null;
    }

    private int GetInteger(JsonData jsonData, string key, int defaultValue = 0)
    {
        if (jsonData.Keys.Contains(key) && jsonData[key] != null)
        {
            int value = 0;
            int.TryParse(jsonData[key].ToString(), out value);
            return value;
        }

        Debugger.Warning($"Missing or invalid key: '{key}' in JSON data. Returning default value: {defaultValue}. " +
                         $"Ensure that the key exists and is a valid integer.");
        return defaultValue;
    }

    private string GetString(JsonData jsonData, string key, string defaultValue = "")
    {
        if (jsonData.Keys.Contains(key) && jsonData[key] != null)
        {
            return jsonData[key].ToString();
        }

        Debugger.Warning($"Missing or invalid key: '{key}' in JSON data. Returning default value: {defaultValue}. " +
                         $"Ensure that the key exists and is a valid string.");
        return defaultValue;
    }
}