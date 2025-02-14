using UnityEngine;

public class MainGameViewController : UIController
{
    private MainGameView _mainGameView;
    private InventoryView _inventoryView;
    private SkillView _skillView;
    private DeadView _deadView;

    public override void Initialise()
    {
        _mainGameView = new MainGameView();
        GameObject obj = ResourceLoader.Instance.LoadObjFromResources("UI/MainGameView");
        _mainGameView.SetObject(obj);
        UIManager.Instance.SetObjectToLayer(obj.transform, UILayer.Middle);
        _mainGameView.Show();

        EventDispatcher.OnPlayerDead += OnPlayerDead;
    }

    public void ShowGameView()
    {
        _mainGameView.Show();
    }

    public void HideGameView()
    {
        _mainGameView.Hide();
    }

    private void OnPlayerDead()
    {
        CreateDeadView();
        _deadView.Show();
    }

    private void CreateDeadView()
    {
        if (_deadView != null) return;
        _deadView = new DeadView();
        GameObject obj = ResourceLoader.Instance.LoadObjFromResources("UI/DeadView");
        _deadView.SetObject(obj);
        UIManager.Instance.SetObjectToLayer(obj.transform, UILayer.Middle);
    }

    public override void Dispose()
    {
        EventDispatcher.OnPlayerDead -= OnPlayerDead;
        _mainGameView.Dispose();
        _mainGameView = null;
    }
}