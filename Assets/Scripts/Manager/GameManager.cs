public class GameManager : MonoSingleton<GameManager>
{
    private bool _isInitialized;

    private void Awake()
    {
        InitGameSystem();
    }

    private void InitGameSystem()
    {
        if (_isInitialized) return;
        _isInitialized = true;
        AudioManager.Instance.Initialize();
        AudioPool.Instance.Initialize();
        UIManager.Instance.Initialize();
        GameDataManager.Instance.Initialize();
        ResourceLoader.Instance.Initialize();
        SceneManager.Instance.Initialize();
        Inventory.Instance.Initialize();
        JsonManager.Instance.Initialize();
        PlayerPrefsManager.Instance.Initialize();
        DontDestroyOnLoad(gameObject);
        UIManager.Instance.StartMenuView();
    }

    public void ChangeToMainMenuScene()
    {
        UIManager.Instance.ShowLoadingView();
        UIManager.Instance.StartMenuView();
        SceneManager.Instance.LoadSceneAsync("MainMenuScene", () => { UIManager.Instance.HideLoadingView(); });
    }

    public void ChangeToGameSceneForest()
    {
        UIManager.Instance.ShowLoadingView();
        UIManager.Instance.StartGameView();
        SceneManager.Instance.LoadSceneAsync("GameSceneForest", () => { UIManager.Instance.HideLoadingView(); });
    }

    public void ChangeToGameSceneCity()
    {
        UIManager.Instance.ShowLoadingView();
        UIManager.Instance.StartGameView();
        SceneManager.Instance.LoadSceneAsync("GameSceneCity", () => { UIManager.Instance.HideLoadingView(); });
    }
}