using UnityEngine.UI;

public class MainGameView : UIBehaviour
{
    private Button _homeBtn;
    private Button _inventoryBtn;
    private Button _skillBtn;
    private Button _settingBtn;

    protected override void ParseComponent()
    {
        _homeBtn = FindComponent<Button>("Middle/Home");
        _inventoryBtn = FindComponent<Button>("Middle/Inventory");
        _skillBtn = FindComponent<Button>("Middle/Skill");
        _settingBtn = FindComponent<Button>("Middle/Setting");
    }

    protected override void AddEvent()
    {
        base.AddEvent();
        RegisterButtonEvent(_homeBtn, OnClickHomeBtn);
        RegisterButtonEvent(_inventoryBtn, OnClickInventoryBtn);
        RegisterButtonEvent(_skillBtn, OnClickSkillBtn);
        RegisterButtonEvent(_settingBtn, OnClickSettingBtn);
    }

    private void OnClickHomeBtn()
    {
    }

    private void OnClickInventoryBtn()
    {
    }

    private void OnClickSkillBtn()
    {
    }

    private void OnClickSettingBtn()
    {
    }

    public override void Dispose()
    {
        UnregisterButtonEvent(_homeBtn, OnClickHomeBtn);
        UnregisterButtonEvent(_inventoryBtn, OnClickInventoryBtn);
        UnregisterButtonEvent(_skillBtn, OnClickSkillBtn);
        UnregisterButtonEvent(_settingBtn, OnClickSettingBtn);
    }
}