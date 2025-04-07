using UnityEngine.UI;

public class MenuSettingView : UIBehaviour
{
    private Slider _musicSlider;
    private Slider _soundSlider;
    private Button _closeBtn;
    private Button _saveBtn;
    private Button _clearBtn;
    private IDataProvider _dataProvider;

    public MenuSettingView(IDataProvider dataProvider)
    {
         _dataProvider = dataProvider;
    }

    protected override void ParseComponent()
    {
        _musicSlider = FindComponent<Slider>("Middle/MusicSlider");
        _soundSlider = FindComponent<Slider>("Middle/SoundSlider");
        _closeBtn = FindComponent<Button>("Top/Button_Back");
        _saveBtn = FindComponent<Button>("Top/Save");
        _clearBtn = FindComponent<Button>("Top/Clear");
    }

    protected override void AddEvent()
    {
        base.AddEvent();
        RegisterButtonEvent(_closeBtn, OnClickCloseBtn);
        RegisterButtonEvent(_saveBtn, OnClickSaveBtn);
        RegisterButtonEvent(_clearBtn, OnClickClearBtn);
        _musicSlider.onValueChanged.AddListener(OnMusicSliderValueChanged);
        _soundSlider.onValueChanged.AddListener(OnSoundSliderValueChanged);
    }

    private void OnClickClearBtn()
    {
        _dataProvider.ClearJSONData();
        _dataProvider.DeleteJSONFile();
    }

    private void OnClickSaveBtn()
    {
        _dataProvider.SaveGameData();
    }

    public override void Show()
    {
        base.Show();
        _musicSlider.value = _dataProvider.MusicVolume;
        _soundSlider.value = _dataProvider.SoundVolume;
    }

    private void OnSoundSliderValueChanged(float arg0)
    {
        SoundManager.Instance.UpdateSfxVolume(arg0);
        _dataProvider.SoundVolume = arg0;
    }

    private void OnMusicSliderValueChanged(float arg0)
    {
        SoundManager.Instance.UpdateMusicVolume(arg0);
        _dataProvider.MusicVolume = arg0;
    }

    private void OnClickCloseBtn()
    {
        SoundManager.Instance.PlaySharedSfx(AudioID.SfxButtonClick);
        Hide();
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