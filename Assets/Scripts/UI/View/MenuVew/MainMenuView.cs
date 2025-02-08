using UnityEngine.UI;

public class MainMenuView : UIBehaviour
{
    private Button _playBtn;
    private Button _settingBtn;

    protected override void ParseComponent()
    {
        _playBtn = FindComponent<Button>("Play");
        _settingBtn = FindComponent<Button>("Setting");
    }

    protected override void AddEvent()
    {
        base.AddEvent();
        RegisterButtonEvent(_playBtn, OnClickPlayBtn);
        RegisterButtonEvent(_settingBtn, OnClickSettingBtn);
    }

    private void OnClickPlayBtn()
    {
        NotifyViewEvent(UIViewEventConst.MainMenuView.OnClickPlayBtn);
    }

    private void OnClickSettingBtn()
    {
    }

    protected override void RemoveEvent()
    {
        base.RemoveEvent();
        UnregisterButtonEvent(_playBtn, OnClickPlayBtn);
        UnregisterButtonEvent(_settingBtn, OnClickSettingBtn);
    }
}