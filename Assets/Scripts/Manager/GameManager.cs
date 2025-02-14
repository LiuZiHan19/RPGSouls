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
        Logger.Info("Init GameSystem");
        AudioManager.Instance.Initialize();
        AudioPool.Instance.Initialize();
        UIManager.Instance.Initialize();
        GameDataManager.Instance.Initialize();
        ResourceLoader.Instance.Initialize();
        SceneManager.Instance.Initialize();
        Inventory.Instance.Initialize();
        JsonManager.Instance.Initialize();
        PlayerPrefsManager.Instance.Initialize();

        UIManager.Instance.ShowMenuView();
        AudioManager.Instance.PlayBgm("Audio/MenuBgm");

        EventDispatcher.OnClickPlay += OnClickPlayBtn;
        EventDispatcher.OnClickPlayAgain += OnClickPlayAgainBtn;
        EventDispatcher.OnClickReturn += OnClickReturnBtn;
    }

    private void OnClickPlayBtn()
    {
        AudioManager.Instance.StopBgm();
        UIManager.Instance.ShowLoadingView();
        UIManager.Instance.HideMenuView();
        UIManager.Instance.ShowGameView();
        SceneManager.Instance.LoadSceneAsync("GameSceneForest", () =>
        {
            AudioManager.Instance.PlayBgm("Audio/ForestBgm");
            UIManager.Instance.HideLoadingView();
        });
    }

    private void OnClickPlayAgainBtn()
    {
        UIManager.Instance.ShowLoadingView();
        SceneManager.Instance.LoadSceneAsync("GameSceneForest", () => { UIManager.Instance.HideLoadingView(); });
    }

    private void OnClickReturnBtn()
    {
        AudioManager.Instance.StopBgm();
        UIManager.Instance.ShowLoadingView();
        UIManager.Instance.HideGameView();
        UIManager.Instance.ShowMenuView();
        SceneManager.Instance.LoadSceneAsync("MainMenuScene", () =>
        {
            AudioManager.Instance.PlayBgm("Audio/MenuBgm");
            UIManager.Instance.HideLoadingView();
        });
    }

    public void Dispose()
    {
        EventDispatcher.OnClickPlay -= OnClickPlayBtn;
        EventDispatcher.OnClickPlayAgain -= OnClickPlayAgainBtn;
        EventDispatcher.OnClickReturn -= OnClickReturnBtn;
    }
}