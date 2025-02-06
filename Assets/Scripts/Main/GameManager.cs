using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        AudioManager.Instance.Initialize();
        AudioPool.Instance.Initialize();
        GameDataManager.Instance.ParsePlayerData(JsonManager.Instance.LoadData("PlayerData"));
    }
}