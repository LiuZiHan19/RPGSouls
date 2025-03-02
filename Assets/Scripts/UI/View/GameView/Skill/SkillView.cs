using UnityEngine.UI;

public class SkillView : UIBehaviour
{
    private Button _closeBtn;

    protected override void ParseComponent()
    {
        _closeBtn = FindComponent<Button>("Middle/TopBar/Button_Home");
    }

    protected override void AddEvent()
    {
        base.AddEvent();
        RegisterButtonEvent(_closeBtn, OnClickCloseBtn);
    }

    private void OnClickCloseBtn()
    {
        TimeManager.Instance.ResumeTime();
        Hide();
    }

    protected override void RemoveEvent()
    {
        base.RemoveEvent();
        UnRegisterButtonEvent(_closeBtn, OnClickCloseBtn);
    }
}