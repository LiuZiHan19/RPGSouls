using System;

public class GameManager : MonoSingletonDontDes<GameManager>, IDisposable, IGameStatusProvider, IGrimReaperProvider
{
    #region IGrimReaperProvider

    public AlmightyStats GrimReaperStats
    {
        get
        {
            if (m_GrimReaper == null)
            {
                m_GrimReaper = FindObjectOfType<EnemyGrimReaper>();
            }

            return m_GrimReaper.entityStats as AlmightyStats;
        }
    }

    private EnemyGrimReaper m_GrimReaper;

    #endregion

    #region IGameStatusProvider

    private bool m_IsChallengeBoss = false;

    public bool IsChallengeBoss
    {
        get => m_IsChallengeBoss;
        set
        {
            if (m_GrimReaper == null)
            {
                m_GrimReaper = FindObjectOfType<EnemyGrimReaper>();
            }

            if (value)
            {
                m_GrimReaper.stateMachine.ChangeState(m_GrimReaper.BattleState);
                SoundManager.Instance.PlayBgm("Sound/music_boss");
                SoundManager.Instance.PlaySfx("Sound/sfx_evil_voice");
            }
            else
            {
                SoundManager.Instance.PlayBgm("Sound/music_forest");
                m_GrimReaper.stateMachine.ChangeState(m_GrimReaper.IdleState);
            }

            m_IsChallengeBoss = value;
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
                m_IsChallengeBoss = false;
            }

            m_IsBossDead = value;
        }
    }

    public bool IsGamePaused { get; set; }

    #endregion

    private bool m_initialized = false;

    protected override void Awake()
    {
        base.Awake();
        if (m_initialized) return;
        m_initialized = true;
        InitGameSystem();
        RegisterEvent();
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