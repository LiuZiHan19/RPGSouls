using UnityEngine.UI;

public class GameSettingView : UIBehaviour
{
    private Button _resumeBtn;
    private Button _returnBtn;
    private Button _saveBtn;
    private IDataProvider _dataProvider;

    public GameSettingView(IDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    protected override void ParseComponent()
    {
        _resumeBtn = FindComponent<Button>("Middle/Resume");
        _returnBtn = FindComponent<Button>("Middle/Return");
        _saveBtn = FindComponent<Button>("Middle/Save");
    }

    protected override void AddEvent()
    {
        base.AddEvent();
        RegisterButtonEvent(_resumeBtn, OnClickResumeBtn);
        RegisterButtonEvent(_returnBtn, OnClickReturnBtn);
        RegisterButtonEvent(_saveBtn, OnClickSaveBtn);
    }

    private void OnClickReturnBtn()
    {
        Hide();
        TimeManager.Instance.ResumeTime();
        EventSubscriber.FromGameSceneToMenuScene?.Invoke();
    }

    private void OnClickResumeBtn()
    {
        TimeManager.Instance.ResumeTime();
        Hide();
    }

    private void OnClickSaveBtn()
    {
        _dataProvider.SaveGameData();
        GameManager.Instance.ReloadData = true;
    }

    protected override void RemoveEvent()
    {
        base.RemoveEvent();
        UnRegisterButtonEvent(_resumeBtn, OnClickResumeBtn);
        UnRegisterButtonEvent(_returnBtn, OnClickReturnBtn);
        UnRegisterButtonEvent(_saveBtn, OnClickSaveBtn);
    }
}