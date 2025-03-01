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
    }

    private void OnClickPlayBtn()
    {
        EventDispatcher.OnClickPlay?.Invoke();
    }

    private void OnClickSettingBtn()
    {
    }

    protected override void RemoveEvent()
    {
        base.RemoveEvent();
        UnRegisterButtonEvent(_playBtn, OnClickPlayBtn);
        UnRegisterButtonEvent(_settingBtn, OnClickSettingBtn);
        UnRegisterButtonEvent(_exitBtn, OnClickExitBtn);
    }
}