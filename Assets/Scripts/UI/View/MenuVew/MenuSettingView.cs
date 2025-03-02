using UnityEngine.UI;

public class MenuSettingView : UIBehaviour
{
    private Slider _musicSlider;
    private Slider _soundSlider;
    private Button _saveBtn;
    private Button _closeBtn;

    protected override void ParseComponent()
    {
        _musicSlider = FindComponent<Slider>("Middle/MusicSlider");
        _soundSlider = FindComponent<Slider>("Middle/SoundSlider");
        _saveBtn = FindComponent<Button>("Middle/SaveBtn");
        _closeBtn = FindComponent<Button>("Middle/TopBar/Button_Home");
    }

    protected override void AddEvent()
    {
        base.AddEvent();
        RegisterButtonEvent(_saveBtn, OnClickSaveBtn);
        RegisterButtonEvent(_closeBtn, OnClickCloseBtn);
        _musicSlider.onValueChanged.AddListener(OnMusicSliderValueChanged);
        _soundSlider.onValueChanged.AddListener(OnSoundSliderValueChanged);
    }

    private void OnSoundSliderValueChanged(float arg0)
    {
        SoundManager.Instance.UpdateSfxVolume(arg0);
    }

    private void OnMusicSliderValueChanged(float arg0)
    {
        SoundManager.Instance.UpdateMusicVolume(arg0);
    }

    private void OnClickCloseBtn()
    {
        Hide();
    }

    private void OnClickSaveBtn()
    {
        if (GameDataManager.Instance.playerDataModel == null)
        {
            Debugger.Info("PlayerDataModel is null");
            return;
        }

        JsonManager.Instance.SaveJsonDataAsync(GameDataManager.Instance.playerDataModel.GetSaveJsonData().ToString(),
            "PlayerData", () => { Debugger.Info("Save Success"); });
    }

    protected override void RemoveEvent()
    {
        _musicSlider.onValueChanged.RemoveListener(OnMusicSliderValueChanged);
        _soundSlider.onValueChanged.RemoveListener(OnSoundSliderValueChanged);
        UnRegisterButtonEvent(_saveBtn, OnClickSaveBtn);
        UnRegisterButtonEvent(_closeBtn, OnClickCloseBtn);
        base.RemoveEvent();
    }
}