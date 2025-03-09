using UnityEngine.UI;

public class MenuSettingView : UIBehaviour
{
    private Slider _musicSlider;
    private Slider _soundSlider;
    private Button _closeBtn;

    protected override void ParseComponent()
    {
        _musicSlider = FindComponent<Slider>("Middle/MusicSlider");
        _soundSlider = FindComponent<Slider>("Middle/SoundSlider");
        _closeBtn = FindComponent<Button>("Top/Button_Back");
    }

    protected override void AddEvent()
    {
        base.AddEvent();
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

    protected override void RemoveEvent()
    {
        _musicSlider.onValueChanged.RemoveListener(OnMusicSliderValueChanged);
        _soundSlider.onValueChanged.RemoveListener(OnSoundSliderValueChanged);
        UnRegisterButtonEvent(_closeBtn, OnClickCloseBtn);
        base.RemoveEvent();
    }
}