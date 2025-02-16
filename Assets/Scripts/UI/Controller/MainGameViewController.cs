using UnityEngine;

public class MainGameViewController : UIController
{
    private MainGameView _mainGameView;
    private InventoryView _inventoryView;
    private SkillView _skillView;
    private DeadView _deadView;

    public override void Initialise()
    {
        ShowGameView();

        EventDispatcher.OnPlayerDead += ShowDeadView;
    }

    #region Game View

    private void CreateGameView()
    {
        if (_mainGameView != null) return;
        _mainGameView = new MainGameView();
        GameObject obj = ResourceLoader.Instance.LoadObjFromResources("UI/MainGameView");
        _mainGameView.SetDisplayObject(obj);
        _mainGameView.AddUIEvent(OnGameViewAction);
        UIManager.Instance.SetObjectToLayer(obj.transform, UILayer.Middle);
    }

    public void ShowGameView()
    {
        CreateGameView();
        _mainGameView.Show();
    }

    public void HideGameView()
    {
        _mainGameView.Hide();
    }

    private void OnGameViewAction(string evtType, object data)
    {
        switch (evtType)
        {
            case EventConst.OnClickInventory:
                OnClickInventory();
                break;
        }
    }

    private void OnClickInventory()
    {
        TimeManager.Instance.PauseGame();
        HideGameView();
        ShowInventoryView();
    }

    #endregion

    #region Dead View

    private void CreateDeadView()
    {
        if (_deadView != null) return;
        _deadView = new DeadView();
        GameObject obj = ResourceLoader.Instance.LoadObjFromResources("UI/DeadView");
        _deadView.SetDisplayObject(obj);
        UIManager.Instance.SetObjectToLayer(obj.transform, UILayer.Middle);
    }

    private void ShowDeadView()
    {
        CreateDeadView();
        _deadView.Show();
    }

    private void HideDeadView()
    {
        _deadView.Hide();
    }

    #endregion

    #region Inventory View

    private void CreateInventoryView()
    {
        if (_inventoryView != null) return;
        _inventoryView = new InventoryView();
        GameObject obj = ResourceLoader.Instance.LoadObjFromResources("UI/InventoryView");
        _inventoryView.SetDisplayObject(obj);
        _inventoryView.AddUIEvent(OnInventoryViewAction);
        UIManager.Instance.SetObjectToLayer(obj.transform, UILayer.Middle);
    }

    private void ShowInventoryView()
    {
        CreateInventoryView();
        _inventoryView.Show();
    }

    private void HideInventoryView()
    {
        _inventoryView.Hide();
    }

    private void OnInventoryViewAction(string evtType, object data)
    {
        switch (evtType)
        {
            case EventConst.OnClickCloseInventory:
                OnClickCloseInventory();
                break;
        }
    }

    private void OnClickCloseInventory()
    {
        TimeManager.Instance.ResumeGame();
        HideInventoryView();
        ShowGameView();
    }

    #endregion

    public override void Dispose()
    {
        EventDispatcher.OnPlayerDead -= ShowDeadView;

        _deadView?.Dispose();
        _deadView = null;

        _skillView?.Dispose();
        _skillView = null;

        _inventoryView.RemoveUIEvent(OnInventoryViewAction);
        _inventoryView?.Dispose();
        _inventoryView = null;

        _mainGameView.RemoveUIEvent(OnGameViewAction);
        _mainGameView?.Dispose();
        _mainGameView = null;
    }
}