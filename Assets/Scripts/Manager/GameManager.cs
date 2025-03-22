using System;
using UnityEngine;

public class GameManager : MonoSingletonDontDes<GameManager>, IDisposable
{
    public bool initialized = false;

    protected override void Awake()
    {
        base.Awake();
        if (initialized) return;
        initialized = true;
        InitGameSystem();
    }

    private void InitGameSystem()
    {
        Debugger.Info("Init GameSystem");
        SoundManager.Instance.Initialize();
        SoundPool.Instance.Initialize();
        UIManager.Instance.Initialize();
        ResourceLoader.Instance.Initialize();
        SceneManager.Instance.Initialize();
        JSONManager.Instance.Initialize();
        PlayerPrefsManager.Instance.Initialize();

        UIManager.Instance.ShowMenuView();
        SoundManager.Instance.PlayBgm("Sound/music_menu");

        GameEventDispatcher.OnClickPlayBtn += OnClickPlayBtn;
        GameEventDispatcher.OnClickPlayAgainBtn += OnClickPlayAgainBtn;
        GameEventDispatcher.OnClickReturnBtn += OnClickReturnBtn;
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