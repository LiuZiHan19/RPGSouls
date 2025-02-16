using UnityEngine;

public class MainMenuViewController : UIController
{
    private MainMenuView _mainMenuView;

    public override void Initialise()
    {
        ShowMenuView();
    }

    #region Menu View

    public void ShowMenuView()
    {
        CreateMenuView();
        _mainMenuView.Show();
    }

    public void HideMenuView()
    {
        _mainMenuView.Hide();
    }

    private void CreateMenuView()
    {
        _mainMenuView = new MainMenuView();
        GameObject mainMenuViewObj = ResourceLoader.Instance.LoadObjFromResources("UI/MainMenuView");
        _mainMenuView.SetDisplayObject(mainMenuViewObj);
        UIManager.Instance.SetObjectToLayer(mainMenuViewObj.transform, UILayer.Top);
    }

    #endregion

    public override void Dispose()
    {
        _mainMenuView?.Dispose();
        _mainMenuView = null;
    }
}