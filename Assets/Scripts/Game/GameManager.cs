using System;
using LitJson;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        AudioManager.Instance.Initialize();
        AudioPool.Instance.Initialize();
        GameDataManager.Instance.ParsePlayerData(JsonManager.Instance.LoadData("PlayerData"));
        
        Logger.Info("Info message");
        Logger.Warning("Warning message");
        Logger.Error("Error message");
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            AudioManager.Instance.PlaySfx("Click");
        }
    }
}