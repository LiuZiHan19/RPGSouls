using UnityEngine;

public class MainMenuViewController : UIController
{
    public MainMenuView mainMenuView;

    public override void Initialise()
    {
        mainMenuView = new MainMenuView();
        GameObject mainMenuViewObj = ResourceLoader.Instance.LoadObjFromResources("MainMenuView");
        mainMenuView.SetObject(mainMenuViewObj);
        mainMenuView.AddUIEvent(OnMainMenuViewAction);
        UIManager.Instance.SetObjectToLayer(mainMenuViewObj.transform, UILayer.Top);
    }

    private void OnMainMenuViewAction(string evtType, object data)
    {
        switch (evtType)
        {
            case UIViewEventConst.MainMenuView.OnClickPlayBtn:
                UIManager.Instance.StartGameView();
                break;
        }
    }

    public override void Dispose()
    {
        mainMenuView.RemoveUIEvent(OnMainMenuViewAction);
        mainMenuView.Dispose();
        mainMenuView = null;
    }
}