using UnityEngine;

public class GameResources : MonoBehaviour
{
    private static GameResources instance;

    public static GameResources Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<GameResources>("GameResources");
                DontDestroyOnLoad(instance);
            }

            return instance;
        }
    }

    [Header("InventoryConfiguration")] public InventoryConfigurationSO inventoryConfigurationSO;
}