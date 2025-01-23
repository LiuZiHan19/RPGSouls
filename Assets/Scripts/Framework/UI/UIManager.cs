public class UIManager : Singleton<UIManager>
{
    private GameViewController _gameViewController;

    public void Initialize()
    {
        _gameViewController = new GameViewController();
    }
}