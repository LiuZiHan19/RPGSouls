public class Singleton<T> where T : new()
{
    private static T instance;
    private static object lockObj = new object();

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lockObj)
                {
                    if (instance == null)
                    {
                        instance = new T();
                    }
                }
            }

            return instance;
        }
    }

    public virtual void Initialize()
    {
    }
}