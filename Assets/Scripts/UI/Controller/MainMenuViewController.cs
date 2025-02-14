using UnityEngine;

public class MainMenuViewController : UIController
{
    private MainMenuView _mainMenuView;

    public override void Initialise()
    {
        _mainMenuView = new MainMenuView();
        GameObject mainMenuViewObj = ResourceLoader.Instance.LoadObjFromResources("UI/MainMenuView");
        _mainMenuView.SetObject(mainMenuViewObj);
        UIManager.Instance.SetObjectToLayer(mainMenuViewObj.transform, UILayer.Top);
    }

    public void ShowMenuView()
    {
        _mainMenuView.Show();
    }

    public void HideMenuView()
    {
        _mainMenuView.Hide();
    }

    public override void Dispose()
    {
        _mainMenuView.Dispose();
        _mainMenuView = null;
    }
}