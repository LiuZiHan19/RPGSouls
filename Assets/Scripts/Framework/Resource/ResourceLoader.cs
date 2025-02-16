using UnityEngine;

public class ResourceLoader : Singleton<ResourceLoader>
{
    public T LoadFromResources<T>(string path) where T : Object
    {
        T t = Resources.Load<T>(path);
        if (t == null)
            Debugger.Error($"Failed to load asset at path: {path}. Type: {typeof(T).Name} not found.");
        return t;
    }

    public Object LoadFromResources(string path)
    {
        Object asset = Resources.Load(path);
        if (asset == null)
            Debugger.Error($"Failed to load asset at path: {path}. Asset not found.");
        return asset;
    }

    public GameObject LoadObjFromResources(string path)
    {
        GameObject obj = Resources.Load<GameObject>(path);
        if (obj == null)
            Debugger.Error($"Failed to load asset at path: {path}. Asset not found.");
        obj = GameObject.Instantiate(obj);
        return obj;
    }
}