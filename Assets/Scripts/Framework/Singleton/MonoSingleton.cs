using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject();
                T t = obj.AddComponent<T>();
                instance = t;
                DontDestroyOnLoad(obj);
            }

            return instance;
        }
    }
}