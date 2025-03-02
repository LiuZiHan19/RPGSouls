using UnityEngine.UI;

public class MainGameView : UIBehaviour
{
    private Button _inventoryBtn;
    private Button _skillBtn;
    private Button _settingBtn;

    protected override void ParseComponent()
    {
        _inventoryBtn = FindComponent<Button>("Middle/OptionList/Inventory");
        _skillBtn = FindComponent<Button>("Middle/OptionList/Skill");
        _settingBtn = FindComponent<Button>("Middle/OptionList/Setting");
    }

    protected override void AddEvent()
    {
        base.AddEvent();
        RegisterButtonEvent(_inventoryBtn, OnClickInventoryBtn);
        RegisterButtonEvent(_skillBtn, OnClickSkillBtn);
        RegisterButtonEvent(_settingBtn, OnClickSettingBtn);
    }

    private void OnClickCraftBtn()
    {
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
    }
}