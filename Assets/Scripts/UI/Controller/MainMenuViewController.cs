using UnityEngine;

public class MainMenuViewController : UIController
{
    private MainMenuView _mainMenuView;

    public override void Initialise()
    {
        _mainMenuView = new MainMenuView();
        GameObject mainMenuViewObj = ResourceLoader.Instance.LoadObjFromResources("MainMenuView");
        _mainMenuView.SetObject(mainMenuViewObj);
        _mainMenuView.AddUIEvent(OnMainMenuViewAction);
        UIManager.Instance.SetObjectToLayer(mainMenuViewObj.transform, UILayer.Top);
    }

    private void OnMainMenuViewAction(string evtType, object data)
    {
        switch (evtType)
        {
            case UIEventConst.MainMenuView.OnClickPlayBtn:
                GameManager.Instance.ChangeToGameSceneForest();
                _mainMenuView.Hide();
                break;
        }
    }

    public override void Dispose()
    {
        _mainMenuView.RemoveUIEvent(OnMainMenuViewAction);
        _mainMenuView.Dispose();
        _mainMenuView = null;
    }
}