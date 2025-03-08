using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : UIBehaviour
{
    private Button _playBtn;
    private Button _settingBtn;
    private Button _exitBtn;

    protected override void ParseComponent()
    {
        _playBtn = FindComponent<Button>("Middle/Play");
        _settingBtn = FindComponent<Button>("Middle/Setting");
        _exitBtn = FindComponent<Button>("Middle/Exit");
    }

    protected override void AddEvent()
    {
        base.AddEvent();
        RegisterButtonEvent(_playBtn, OnClickPlayBtn);
        RegisterButtonEvent(_settingBtn, OnClickSettingBtn);
        RegisterButtonEvent(_exitBtn, OnClickExitBtn);
    }

    private void OnClickExitBtn()
    {
        Application.Quit();
    }

    private void OnClickPlayBtn()
    {
        GameEventDispatcher.OnClickPlayBtn?.Invoke();
    }

    private void OnClickSettingBtn()
    {
        NotifyViewEvent(EventConst.OnClickMenuSetting);
    }

    protected override void RemoveEvent()
    {
        base.RemoveEvent();
        UnRegisterButtonEvent(_playBtn, OnClickPlayBtn);
        UnRegisterButtonEvent(_settingBtn, OnClickSettingBtn);
        UnRegisterButtonEvent(_exitBtn, OnClickExitBtn);
    }
}