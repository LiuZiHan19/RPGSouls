using UnityEngine.UI;

public class GameWinView : UIBehaviour
{
    private Button _resumeBtn;
    private Button _returnBtn;
    private Button _saveBtn;

    protected override void ParseComponent()
    {
        _resumeBtn = FindComponent<Button>("Middle/PlayAgain");
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
        GameEventDispatcher.OnClickReturnBtn?.Invoke();
    }

    private void OnClickResumeBtn()
    {
        TimeManager.Instance.ResumeTime();
        Hide();
    }

    private void OnClickSaveBtn()
    {
        GameDataManager.Instance.SavePlayerData(() =>
        {
            Debugger.Info($"[GameSetting] {nameof(GameDataManager.SavePlayerData)} Success");
        });

        GameDataManager.Instance.SaveInventoryData(() =>
        {
            Debugger.Info($"[GameSetting] {nameof(GameDataManager.SaveInventoryData)} Success");
        });

        GameDataManager.Instance.SaveSkillData(() =>
        {
            Debugger.Info($"[GameSetting] {nameof(GameDataManager.SaveSkillData)} Success");
        });
    }

    protected override void RemoveEvent()
    {
        base.RemoveEvent();
        UnRegisterButtonEvent(_resumeBtn, OnClickResumeBtn);
        UnRegisterButtonEvent(_returnBtn, OnClickReturnBtn);
        UnRegisterButtonEvent(_saveBtn, OnClickSaveBtn);
    }
}