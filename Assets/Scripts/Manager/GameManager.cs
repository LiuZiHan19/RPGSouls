using System;
using UnityEngine;

public class GameManager : MonoBehaviour, IDisposable, IGrimReaperProvider
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    private EnemyGrimReaper m_grimReaper;
    public EnemyGrimReaper GrimReaper => m_grimReaper;
    public bool IsGrimReaperDead { get; set; }

    private bool m_initialized;
    private uint m_menuBgm;
    private uint m_gameBgm;
    private uint m_bossBgm;
    private ulong m_evilVoiceSfx;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (m_initialized) return;
        m_initialized = true;
        InitGameSystem();
        RegisterEvent();
    }

    private void InitGameSystem()
    {
#if UNITY_EDITOR
        SoundManager.Instance.InitialiseHierarchyWindow();
#endif
        SoundManager.Instance.PlayBGM(AudioID.MenuBGM, ref m_menuBgm);
        GameDataManager.Instance.LoadGameData();
        UIManager.Instance.CreateGameUI();
    }

    private void RegisterEvent()
    {
        GameEventDispatcher.OnClickPlayBtn += FromMenuSceneToGameScene;
        GameEventDispatcher.OnClickPlayAgainBtn += ReloadGameScene;
        GameEventDispatcher.OnClickReturnBtn += FromGameSceneToMenuScene;
    }

    public void ChallengeBoss(bool isChallenge)
    {
        if (isChallenge)
        {
            SoundManager.Instance.PauseBGM(m_gameBgm);
            SoundManager.Instance.PlayBGM(AudioID.BossBGM, ref m_bossBgm);
            if (m_grimReaper != null)
                m_grimReaper.stateMachine.ChangeState(m_grimReaper.BattleState);
        }
        else
        {
            SoundManager.Instance.PauseBGM(m_bossBgm);
            SoundManager.Instance.PlayBGM(AudioID.GameBGM, ref m_gameBgm);
            if (m_grimReaper != null)
                m_grimReaper.stateMachine.ChangeState(m_grimReaper.IdleState);
        }
    }

    private void FromMenuSceneToGameScene()
    {
        SoundManager.Instance.PauseBGM(m_menuBgm);
        UIManager.Instance.ShowLoadingView();
        UIManager.Instance.HideMenuView();
        UIManager.Instance.ShowGameView();
        SceneLoader.Instance.LoadSceneAsync(SceneID.MainGameScene, () =>
        {
            m_grimReaper = FindObjectOfType<EnemyGrimReaper>();
            SoundManager.Instance.PlayBGM(AudioID.GameBGM, ref m_gameBgm);
            UIManager.Instance.HideLoadingView();
        });
    }

    private void FromGameSceneToMenuScene()
    {
        SoundManager.Instance.PauseBGM(m_gameBgm);
        UIManager.Instance.ShowLoadingView();
        UIManager.Instance.HideGameView();
        UIManager.Instance.ShowMenuView();
        SoundManager.Instance.PlaySfx(AudioID.EvilVoiceSfx, ref m_evilVoiceSfx);
        SoundManager.Instance.PauseSfx(m_evilVoiceSfx);
        SceneLoader.Instance.LoadSceneAsync(SceneID.MainMenuScene, () =>
        {
            SoundManager.Instance.PlayBGM(AudioID.MenuBGM, ref m_menuBgm);
            UIManager.Instance.HideLoadingView();
        });
    }

    private void ReloadGameScene()
    {
        UIManager.Instance.ShowLoadingView();
        SceneLoader.Instance.LoadSceneAsync(SceneID.MainGameScene, () => { UIManager.Instance.HideLoadingView(); });
    }

    ~GameManager()
    {
        Dispose();
    }

    public void Dispose()
    {
        GameEventDispatcher.OnClickPlayBtn -= FromMenuSceneToGameScene;
        GameEventDispatcher.OnClickPlayAgainBtn -= ReloadGameScene;
        GameEventDispatcher.OnClickReturnBtn -= FromGameSceneToMenuScene;
    }
}