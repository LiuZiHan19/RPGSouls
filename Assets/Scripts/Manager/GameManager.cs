using System;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour, IDisposable, IGrimReaperProvider, IDataProvider
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    public bool ResetPlayerHealth { get; set; }
    public bool ReloadData { get; set; }

    #region IDataProvider

    public int Coin
    {
        get => DataManager.Instance.GameDataModel.coin;
        set => DataManager.Instance.GameDataModel.coin = value;
    }

    public float SoundVolume
    {
        get => DataManager.Instance.GameDataModel.soundVolume;
        set => DataManager.Instance.GameDataModel.soundVolume = value;
    }

    public float MusicVolume
    {
        get => DataManager.Instance.GameDataModel.musicVolume;
        set => DataManager.Instance.GameDataModel.musicVolume = value;
    }

    public void SaveGameData(UnityAction callback = null)
    {
        DataManager.Instance.SaveGameData(callback);
    }

    #endregion

    #region IGrimReaperProvider

    private EnemyGrimReaper m_grimReaper;
    public EnemyGrimReaper GrimReaper => m_grimReaper;
    public bool IsGrimReaperDead { get; set; }

    #endregion

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
        RegisterEvent();
        CreateGameWorld();
    }

    private void CreateGameWorld()
    {
#if UNITY_EDITOR
        SoundManager.Instance.InitialiseHierarchyWindow();
#endif
        DataManager.Instance.LoadData(() =>
        {
            SoundManager.Instance.PlayBGM(AudioID.MenuBGM, ref m_menuBgm);
            UIManager.Instance.CreateGameUI();
        });
    }

    private void RegisterEvent()
    {
        EventDispatcher.OnClickPlayBtn += FromMenuSceneToGameScene;
        EventDispatcher.OnClickReturnBtn += FromGameSceneToMenuScene;
        EventDispatcher.OnClickPlayAgainBtn += ReloadGameScene;
    }

    private void FromMenuSceneToGameScene()
    {
        SoundManager.Instance.StopBGM(m_menuBgm);

        UIManager.Instance.ShowLoadingView();
        UIManager.Instance.HideMenuView();
        UIManager.Instance.ShowGameView();

        SceneLoader.Instance.LoadSceneAsync(SceneID.MainGameScene, () =>
        {
            SoundManager.Instance.PlayBGM(AudioID.GameBGM, ref m_gameBgm);
            UIManager.Instance.HideLoadingView();
        });
    }

    private void FromGameSceneToMenuScene()
    {
        SoundManager.Instance.StopBGM(m_gameBgm);

        UIManager.Instance.ShowLoadingView();
        UIManager.Instance.HideGameView();
        UIManager.Instance.CreateMenuView();

        CheckLoadDataAndLoadGameScene();
    }

    private void ReloadGameScene()
    {
        SoundManager.Instance.StopBGM(m_gameBgm);

        UIManager.Instance.ShowLoadingView();

        CheckLoadDataAndLoadGameScene();
    }

    private void CheckLoadDataAndLoadGameScene()
    {
        if (ReloadData)
        {
            ReloadData = false;
            DataManager.Instance.LoadData(() => { LoadGameScene(); });
        }
        else
        {
            LoadGameScene();
        }
    }

    private void LoadGameScene()
    {
        SceneLoader.Instance.LoadSceneAsync(SceneID.MainMenuScene, () =>
        {
            SoundManager.Instance.PlayBGM(AudioID.MenuBGM, ref m_menuBgm);
            UIManager.Instance.HideLoadingView();
        });
    }

    public void ChallengeBoss(bool isChallenge)
    {
        if (isChallenge)
        {
            m_grimReaper = FindObjectOfType<EnemyGrimReaper>();
            SoundManager.Instance.StopBGM(m_gameBgm);
            SoundManager.Instance.PlayBGM(AudioID.BossBGM, ref m_bossBgm);
            SoundManager.Instance.PlaySfx(AudioID.EvilVoiceSfx, ref m_evilVoiceSfx);
            if (m_grimReaper != null)
                m_grimReaper.stateMachine.ChangeState(m_grimReaper.BattleState);
        }
        else
        {
            SoundManager.Instance.StopBGM(m_bossBgm);
            SoundManager.Instance.StopSfx(m_evilVoiceSfx);
            SoundManager.Instance.PlayBGM(AudioID.GameBGM, ref m_gameBgm);
            if (m_grimReaper != null)
                m_grimReaper.stateMachine.ChangeState(m_grimReaper.IdleState);
        }
    }

    public void Dispose()
    {
        EventDispatcher.OnClickPlayBtn -= FromMenuSceneToGameScene;
        EventDispatcher.OnClickPlayAgainBtn -= ReloadGameScene;
        EventDispatcher.OnClickReturnBtn -= FromGameSceneToMenuScene;
    }
}