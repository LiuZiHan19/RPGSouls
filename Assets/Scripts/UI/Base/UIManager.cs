using System;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public Canvas UICanvas;
    public Camera UICamera;
    public Transform Bottom;
    public Transform Middle;
    public Transform Top;
    private MainMenuViewController _mainMenuViewController;
    private MainGameViewController _mainGameViewController;
    private GeneralLoadingView _generalLoadingView;

    public override void Initialize()
    {
        base.Initialize();
        GameObject uiRoot = ResourceLoader.Instance.LoadObjFromResources("UIRoot");
        UICanvas = uiRoot.transform.Find("UICanvas").GetComponent<Canvas>();
        UICamera = uiRoot.transform.Find("UICamera").GetComponent<Camera>();
        Bottom = uiRoot.transform.Find("UICanvas").transform.Find("Bottom").transform;
        Middle = uiRoot.transform.Find("UICanvas").transform.Find("Middle").transform;
        Top = uiRoot.transform.Find("UICanvas").transform.Find("Top").transform;
    }

    public void StartMenuScene()
    {
        _mainMenuViewController = new MainMenuViewController();
        _mainMenuViewController.Initialise();
    }

    public void StartGameScene()
    {
        ShowLoadingView();
        _mainMenuViewController.Dispose();
        _mainMenuViewController = null;
        _mainGameViewController = new MainGameViewController();
        _mainGameViewController.Initialise();
    }

    private void CreateLoadingView()
    {
        if (_generalLoadingView != null) return;
        _generalLoadingView = new GeneralLoadingView();
        GameObject loadingViewObj = ResourceLoader.Instance.LoadObjFromResources("GeneralLoadingView");
        _generalLoadingView.SetObject(loadingViewObj);
        SetObjectToLayer(loadingViewObj.transform, UILayer.Top);
    }

    private void ShowLoadingView()
    {
        CreateLoadingView();
        _generalLoadingView.Show();
    }

    public void SetObjectToLayer(Transform obj, UILayer layer)
    {
        switch (layer)
        {
            case UILayer.Bottom:
                obj.SetParent(Bottom, false);
                break;
            case UILayer.Middle:
                obj.SetParent(Middle, false);
                break;
            case UILayer.Top:
                obj.SetParent(Top, false);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(layer), layer, null);
        }
    }
}

public enum UILayer
{
    Bottom,
    Middle,
    Top
}

// Depth Only 在物体销毁后不更新画布