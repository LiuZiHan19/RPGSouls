using UnityEngine.UI;

public class MainGameView : UIBehaviour
{
    private Button _inventoryBtn;
    private Button _skillBtn;
    private Button _craftBtn;
    private Button _settingBtn;

    protected override void ParseComponent()
    {
        _inventoryBtn = FindComponent<Button>("Middle/OptionList/Inventory");
        _craftBtn = FindComponent<Button>("Middle/OptionList/Craft");
        _skillBtn = FindComponent<Button>("Middle/OptionList/Skill");
        _settingBtn = FindComponent<Button>("Middle/OptionList/Setting");
    }

    protected override void AddEvent()
    {
        base.AddEvent();
        RegisterButtonEvent(_inventoryBtn, OnClickInventoryBtn);
        RegisterButtonEvent(_skillBtn, OnClickSkillBtn);
        RegisterButtonEvent(_settingBtn, OnClickSettingBtn);
        RegisterButtonEvent(_craftBtn, OnClickCraftBtn);
    }

    private void OnClickCraftBtn()
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
        base.Dispose();
        UnregisterButtonEvent(_inventoryBtn, OnClickInventoryBtn);
        UnregisterButtonEvent(_skillBtn, OnClickSkillBtn);
        UnregisterButtonEvent(_settingBtn, OnClickSettingBtn);
        UnregisterButtonEvent(_craftBtn, OnClickCraftBtn);
    }
}