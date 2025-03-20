using UnityEngine.UI;

public class SkillView : UIBehaviour
{
    private Button _closeBtn;
    private SkillItemView _skill_RollView;
    private SkillItemView _skill_CloneView;
    private SkillItemView _skill_IdleBlockView;

    protected override void ParseComponent()
    {
        _closeBtn = FindComponent<Button>("Top/Button_Back");
        _skill_RollView = new SkillItemView();
        _skill_RollView.SetDisplayObject(FindGameObject("Middle/Skill_Roll"));
        _skill_CloneView = new SkillItemView();
        _skill_CloneView.SetDisplayObject(FindGameObject("Middle/Skill_Clone"));
        _skill_IdleBlockView = new SkillItemView();
        _skill_IdleBlockView.SetDisplayObject(FindGameObject("Middle/Skill_IdleBlock"));

        if (GameDataManager.Instance.SkillDataModel.skillRollData.isUlocked)
        {
            _skill_RollView.Unlock();
        }

        if (GameDataManager.Instance.SkillDataModel.skillCloneData.isUlocked)
        {
            _skill_CloneView.Unlock();
        }

        if (GameDataManager.Instance.SkillDataModel.skillIdleBlockData.isUlocked)
        {
            _skill_IdleBlockView.Unlock();
        }
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