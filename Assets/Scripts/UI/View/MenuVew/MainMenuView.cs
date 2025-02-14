using UnityEngine.UI;

public class MainMenuView : UIBehaviour
{
    private Button _playBtn;
    private Button _settingBtn;
    private Button _exitBtn;

    protected override void ParseComponent()
    {
        _playBtn = FindComponent<Button>("Middle/OptionList/Viewport/Content/Play");
        _settingBtn = FindComponent<Button>("Middle/OptionList/Viewport/Content/Setting");
        _exitBtn = FindComponent<Button>("Middle/OptionList/Viewport/Content/Exit");
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
        UnregisterButtonEvent(_playBtn, OnClickPlayBtn);
        UnregisterButtonEvent(_settingBtn, OnClickSettingBtn);
        UnregisterButtonEvent(_exitBtn, OnClickExitBtn);
    }
}