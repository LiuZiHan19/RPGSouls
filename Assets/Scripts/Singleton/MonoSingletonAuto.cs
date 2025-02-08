using UnityEngine;

public class MonoSingletonAuto<T> : MonoBehaviour where T : MonoBehaviour
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
#if UNITY_EDITOR
                obj.name = typeof(T).Name;
#endif
            }

            return instance;
        }
    }
}