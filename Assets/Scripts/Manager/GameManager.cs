using System;
using UnityEngine;

public class GameManager : MonoBehaviour, IDisposable, IGameStatusProvider, IGrimReaperProvider
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    #region IGrimReaperProvider

    public AlmightyStats GrimReaperStats
    {
        get { return m_GrimReaper.entityStats as AlmightyStats; }
    }

    private EnemyGrimReaper m_GrimReaper;

    #endregion

    #region IGameStatusProvider

    private bool m_isChanged = false;
    private bool m_isChallengeBoss = false;

    public bool IsChallengeBoss
    {
        get => m_isChallengeBoss;
        set
        {
            if (m_GrimReaper == null)
            {
                m_isChallengeBoss = false;
                return;
            }

            m_isChanged = true;
            m_isChallengeBoss = value;
        }
    }

    private bool m_IsBossDead = false;

    public bool IsBossDead
    {
        get => m_IsBossDead;
        set
        {
            if (value)
            {
                SoundManager.Instance.PlayBgm("Sound/music_forest");
                m_isChallengeBoss = false;
            }

            m_IsBossDead = value;
        }
    }

    public bool IsGamePaused { get; set; }

    #endregion

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        InitGameSystem();
        RegisterEvent();
    }

    private void Update()
    {
        if (m_isChanged)
        {
            m_isChanged = false;
            if (m_isChallengeBoss)
            {
                m_GrimReaper.stateMachine.ChangeState(m_GrimReaper.BattleState);
                SoundManager.Instance.PlayBgmAsync("Sound/music_boss");
                SoundManager.Instance.PlaySfxAsync("Sound/sfx_evil_voice");
            }
            else
            {
                SoundManager.Instance.PlayBgmAsync("Sound/music_forest");
                m_GrimReaper.stateMachine.ChangeState(m_GrimReaper.IdleState);
            }
        }
    }

    private void InitGameSystem()
    {
        SoundManager.Instance.Initialize();
        SoundPool.Instance.Initialize();
        UIManager.Instance.Initialize();
        ResourceLoader.Instance.Initialize();
        SceneManager.Instance.Initialize();
        JSONManager.Instance.Initialize();
        PlayerPrefsManager.Instance.Initialize();

        UIManager.Instance.ShowMenuView();
    }

    private void RegisterEvent()
    {
        GameEventDispatcher.OnClickPlayBtn += OnClickPlayBtn;
        GameEventDispatcher.OnClickPlayAgainBtn += OnClickPlayAgainBtn;
        GameEventDispatcher.OnClickReturnBtn += OnClickReturnBtn;
    }

    private void ClearSceneTrash()
    {
        SoundPool.Instance.Dispose();
        FXPool.Instance.Dispose();
        CoroutineManager.Instance.IStopAllCoroutine();
    }

    #region Event

    private void OnClickPlayBtn()
    {
        ClearSceneTrash();
        SoundManager.Instance.StopBgm();
        UIManager.Instance.ShowLoadingView();
        UIManager.Instance.HideMenuView();
        UIManager.Instance.ShowGameView();
        SceneManager.Instance.LoadSceneAsync("MainGameScene", () =>
        {
            m_GrimReaper = FindObjectOfType<EnemyGrimReaper>();
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

    #endregion

    public void Dispose()
    {
        GameEventDispatcher.OnClickPlayBtn -= OnClickPlayBtn;
        GameEventDispatcher.OnClickPlayAgainBtn -= OnClickPlayAgainBtn;
        GameEventDispatcher.OnClickReturnBtn -= OnClickReturnBtn;
    }
}