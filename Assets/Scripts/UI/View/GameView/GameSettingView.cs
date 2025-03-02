using UnityEngine.UI;

public class GameSettingView : UIBehaviour
{
    private Button _resumeBtn;
    private Button _returnBtn;

    protected override void ParseComponent()
    {
        _resumeBtn = FindComponent<Button>("Middle/Resume");
        _returnBtn = FindComponent<Button>("Middle/Return");
    }

    protected override void AddEvent()
    {
        base.AddEvent();
        RegisterButtonEvent(_resumeBtn, OnClickResumeBtn);
        RegisterButtonEvent(_returnBtn, OnClickReturnBtn);
    }

    private void OnClickReturnBtn()
    {
        Hide();
        TimeManager.Instance.ResumeTime();
        EventDispatcher.OnClickReturnBtn?.Invoke();
    }

    private void OnClickResumeBtn()
    {
        TimeManager.Instance.ResumeTime();
        Hide();
    }

    protected override void RemoveEvent()
    {
        base.RemoveEvent();
        UnRegisterButtonEvent(_resumeBtn, OnClickResumeBtn);
        UnRegisterButtonEvent(_returnBtn, OnClickReturnBtn);
    }
}