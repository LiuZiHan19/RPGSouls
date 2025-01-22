using LitJson;
using System.IO;
using UnityEngine;

public enum JsonType
{
    JsonUtlity,
    LitJson,
}

public class JsonManager
{
    private static JsonManager instance = new JsonManager();
    public static JsonManager Instance => instance;

    private JsonManager()
    {
    }

    public void SaveData(object data, string fileName, JsonType type = JsonType.LitJson)
    {
        string path = Application.persistentDataPath + "/" + fileName + ".json";
        string jsonStr = "";
        switch (type)
        {
            case JsonType.JsonUtlity:
                jsonStr = JsonUtility.ToJson(data);
                break;
            case JsonType.LitJson:
                jsonStr = JsonMapper.ToJson(data);
                break;
        }

        File.WriteAllText(path, jsonStr);
    }

    public T LoadData<T>(string fileName, JsonType type = JsonType.LitJson) where T : new()
    {
        string path = Application.streamingAssetsPath + "/" + fileName + ".json";
        if (!File.Exists(path))
            path = Application.persistentDataPath + "/" + fileName + ".json";
        if (!File.Exists(path))
            return new T();

        string jsonStr = File.ReadAllText(path);
        T data = default(T);
        switch (type)
        {
            case JsonType.JsonUtlity:
                data = JsonUtility.FromJson<T>(jsonStr);
                break;
            case JsonType.LitJson:
                data = JsonMapper.ToObject<T>(jsonStr);
                break;
        }

        return data;
    }
}