using UnityEngine.UI;

public class MainGameView : UIBehaviour
{
    private Button _inventoryBtn;
    private Button _skillBtn;
    private Button _settingBtn;
    private Slider _healthSlider;

    protected override void ParseComponent()
    {
        _inventoryBtn = FindComponent<Button>("Middle/OptionList/Inventory");
        _skillBtn = FindComponent<Button>("Middle/OptionList/Skill");
        _settingBtn = FindComponent<Button>("Middle/OptionList/Setting");
        _healthSlider = FindComponent<Slider>("Top/Slider_HealthBar/Slider");
    }

    protected override void AddEvent()
    {
        base.AddEvent();
        RegisterButtonEvent(_inventoryBtn, OnClickInventoryBtn);
        RegisterButtonEvent(_skillBtn, OnClickSkillBtn);
        RegisterButtonEvent(_settingBtn, OnClickSettingBtn);
        GameEventDispatcher.OnPlayerTakeDamage += OnPlayerTakeDamage;
    }

    private void OnPlayerTakeDamage(float percentage)
    {
        _healthSlider.value = percentage;
    }

    private void OnClickInventoryBtn()
    {
        NotifyViewEvent(EventConst.OnClickInventory);
    }

    private void OnClickSkillBtn()
    {
        NotifyViewEvent(EventConst.OnClickSkill);
    }

    private void OnClickSettingBtn()
    {
        NotifyViewEvent(EventConst.OnClickGameSetting);
    }

    public override void Dispose()
    {
        base.Dispose();
        UnRegisterButtonEvent(_inventoryBtn, OnClickInventoryBtn);
        UnRegisterButtonEvent(_skillBtn, OnClickSkillBtn);
        UnRegisterButtonEvent(_settingBtn, OnClickSettingBtn);
        GameEventDispatcher.OnPlayerTakeDamage -= OnPlayerTakeDamage;
    }
}