using UnityEngine;

public class GameResource : MonoBehaviour
{
    private static GameResource instance;

    public static GameResource Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<GameResource>("GameResource");
            }

            return instance;
        }
    }
}