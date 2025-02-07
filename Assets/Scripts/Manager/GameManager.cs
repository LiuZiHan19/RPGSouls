using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        AudioManager.Instance.Initialize();
        AudioPool.Instance.Initialize();
        UIManager.Instance.Initialize();
        GameDataManager.Instance.Initialize();
        ResourceLoader.Instance.Initialize();
        SceneManager.Instance.Initialize();
        Inventory.Instance.Initialize();
        JsonManager.Instance.Initialize();
        PlayerPrefsManager.Instance.Initialize();
    }

    private void Start()
    {
        UIManager.Instance.StartMenuScene();
    }
}