using LitJson;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class JsonManager : Singleton<JsonManager>
{
    /// <summary>
    /// 同步加载Json数据
    /// </summary>
    /// <param name="path"></param>
    /// <returns>JsonData</returns>
    public JsonData LoadData(string path)
    {
        string finalPath = Application.streamingAssetsPath + "/" + path + ".json";
        if (!File.Exists(finalPath)) finalPath = Application.persistentDataPath + "/" + path + ".json";

        string jsonStr = File.ReadAllText(finalPath);
        JsonData jsonData = new JsonData(jsonStr);
        jsonData = JsonMapper.ToObject(jsonStr);

        return jsonData;
    }

    /// <summary>
    /// 异步加载 JSON 数据并执行回调
    /// </summary>
    /// <param name="path">JSON 文件路径（相对路径，不包含扩展名）</param>
    /// <param name="callback">加载完成后执行的回调</param>
    /// <returns>返回解析后的 JsonData</returns>
    public async Task<JsonData> LoadDataAsync(string path, UnityAction<JsonData> callback = null)
    {
        string finalPath = Application.streamingAssetsPath + "/" + path + ".json";
        if (File.Exists(finalPath) == false) finalPath = Application.persistentDataPath + "/" + path + ".json";

        if (File.Exists(finalPath) == false) Logger.Error("文件未找到: " + finalPath);
        string json = await File.ReadAllTextAsync(finalPath);
        JsonData jsonData = JsonMapper.ToObject(json);
        callback?.Invoke(jsonData);
        return jsonData;
    }

    /// <summary>
    /// 读取Json数据转换为类对象(适合简单类对象)
    /// </summary>
    /// <param name="path"></param>
    /// <param name="type"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T LoadData<T>(string path, E_Json type = E_Json.LitJson) where T : new()
    {
        string finalPath = Application.streamingAssetsPath + "/" + path + ".json";
        if (!File.Exists(finalPath))
            finalPath = Application.persistentDataPath + "/" + path + ".json";
        if (!File.Exists(finalPath))
            return new T();

        string jsonStr = File.ReadAllText(finalPath);
        JsonData jsonData = new JsonData(jsonStr);
        T data = default(T);
        switch (type)
        {
            case E_Json.JsonUtility:
                data = JsonUtility.FromJson<T>(jsonStr);
                break;
            case E_Json.LitJson:
                data = JsonMapper.ToObject<T>(jsonStr);
                break;
        }

        return data;
    }

    /// <summary>
    /// 将类对象转换为Json数据存储(适合简单类对象)
    /// </summary>
    /// <param name="path"></param>
    /// <param name="type"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public void SaveData(object data, string path, E_Json type = E_Json.LitJson)
    {
        string finalPath = Application.persistentDataPath + "/" + path + ".json";
        string jsonStr = "";
        switch (type)
        {
            case E_Json.JsonUtility:
                jsonStr = JsonUtility.ToJson(data);
                break;
            case E_Json.LitJson:
                jsonStr = JsonMapper.ToJson(data);
                break;
        }

        File.WriteAllText(finalPath, jsonStr);
    }

    public void SaveData(string data, string path)
    {
        string finalPath = Application.persistentDataPath + "/" + path + ".json";
        string jsonStr = data;
        File.WriteAllText(finalPath, jsonStr);
    }
}