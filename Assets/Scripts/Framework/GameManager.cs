using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameData _gameData;

    private void Awake()
    {
        _gameData = JsonManager.Instance.LoadData<GameData>("GameData");
    }

    private void Start()
    {
    }
}