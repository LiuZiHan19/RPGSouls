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

        EventDispatcher.OnClickPlayBtn += OnClickPlayBtn;
        EventDispatcher.OnClickPlayAgainBtn += OnClickPlayAgainBtn;
        EventDispatcher.OnClickReturnBtn += OnClickReturnBtn;
    }

    private void OnClickPlayBtn()
    {
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
        UIManager.Instance.ShowLoadingView();
        SceneManager.Instance.LoadSceneAsync("MainGameScene", () => { UIManager.Instance.HideLoadingView(); });
    }

    private void OnClickReturnBtn()
    {
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

    public void Dispose()
    {
        EventDispatcher.OnClickPlayBtn -= OnClickPlayBtn;
        EventDispatcher.OnClickPlayAgainBtn -= OnClickPlayAgainBtn;
        EventDispatcher.OnClickReturnBtn -= OnClickReturnBtn;
        InventoryManager.Instance?.Dispose();
    }
}