using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : UIBehaviour
{
    private Button _playBtn;
    private Button _settingBtn;
    private Button _exitBtn;
    private Text _coinText;

    protected override void ParseComponent()
    {
        _coinText = FindComponent<Text>("Middle/Status/Status_Gold/Text");
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

    public override void Show()
    {
        base.Show();
        _coinText.text = DataManager.Instance.GameDataModel.coin.ToString();
    }

    private void OnClickExitBtn()
    {
        GameManager.Instance.Dispose();
        Application.Quit();
    }

    private void OnClickPlayBtn()
    {
        EventDispatcher.OnClickPlayBtn?.Invoke();
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