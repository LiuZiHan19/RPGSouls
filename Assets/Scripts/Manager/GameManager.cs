using System;
using UnityEngine;

public class GameManager : MonoBehaviour, IDisposable
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        InitGameSystem();
        LoadGameData();
    }

    private void InitGameSystem()
    {
        Debugger.Info("Init GameSystem");
        SoundManager.Instance.Initialize();
        SoundPool.Instance.Initialize();
        UIManager.Instance.Initialize();
        GameDataManager.Instance.Initialize();
        ResourceLoader.Instance.Initialize();
        SceneManager.Instance.Initialize();
        JsonManager.Instance.Initialize();
        PlayerPrefsManager.Instance.Initialize();
        EventManager.Instance.Initialize();

        UIManager.Instance.ShowMenuView();
        SoundManager.Instance.PlayBgm("Sound/music_menu");

        GameEventDispatcher.OnClickPlayBtn += OnClickPlayBtn;
        GameEventDispatcher.OnClickPlayAgainBtn += OnClickPlayAgainBtn;
        GameEventDispatcher.OnClickReturnBtn += OnClickReturnBtn;
    }

    private void LoadGameData()
    {
        Debugger.Info("Load GameData");
        GameDataManager.Instance.LoadPlayerData();
        GameDataManager.Instance.LoadInventoryData();
    }

    private void OnClickPlayBtn()
    {
        ClearSceneTrash();
        SoundManager.Instance.StopBgm();
        UIManager.Instance.ShowLoadingView();
        UIManager.Instance.HideMenuView();
        UIManager.Instance.ShowGameView();
        SceneManager.Instance.LoadSceneAsync("MainGameScene", () =>
        {
            SoundManager.Instance.PlayBgm("Sound/music_forest");
            UIManager.Instance.HideLoadingView();
        });
    }

    private void OnClickPlayAgainBtn()
    {
        ClearSceneTrash();
        UIManager.Instance.ShowLoadingView();
        SceneManager.Instance.LoadSceneAsync("MainGameScene", () => { UIManager.Instance.HideLoadingView(); });
    }

    private void OnClickReturnBtn()
    {
        ClearSceneTrash();
        SoundManager.Instance.StopBgm();
        UIManager.Instance.ShowLoadingView();
        UIManager.Instance.HideGameView();
        UIManager.Instance.ShowMenuView();
        SceneManager.Instance.LoadSceneAsync("MainMenuScene", () =>
        {
            SoundManager.Instance.PlayBgm("Sound/music_menu");
            UIManager.Instance.HideLoadingView();
        });
    }

    private void ClearSceneTrash()
    {
        FXPool.Instance.Clear();
        CoroutineManager.Instance.IStopAllCoroutine();
    }

    public void Dispose()
    {
        GameEventDispatcher.OnClickPlayBtn -= OnClickPlayBtn;
        GameEventDispatcher.OnClickPlayAgainBtn -= OnClickPlayAgainBtn;
        GameEventDispatcher.OnClickReturnBtn -= OnClickReturnBtn;
    }
}