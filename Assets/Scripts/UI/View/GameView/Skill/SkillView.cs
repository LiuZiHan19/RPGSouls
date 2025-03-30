using UnityEngine.UI;

public class SkillView : UIBehaviour
{
    private Button _closeBtn;
    private SkillItemView _skill_RollView;
    private SkillItemView _skill_CloneView;
    private SkillItemView _skill_IdleBlockView;
    private Text _coinText;
    private IDataProvider _dataProvider;

    public SkillView(IDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    protected override void ParseComponent()
    {
        _closeBtn = FindComponent<Button>("Top/Button_Back");
        _skill_RollView = new SkillItemView(GameManager.Instance);
        _skill_RollView.SetDisplayObject(FindGameObject("Middle/Skill_Roll"));
        _skill_CloneView = new SkillItemView(GameManager.Instance);
        _skill_CloneView.SetDisplayObject(FindGameObject("Middle/Skill_Clone"));
        _skill_IdleBlockView = new SkillItemView(GameManager.Instance);
        _skill_IdleBlockView.SetDisplayObject(FindGameObject("Middle/Skill_IdleBlock"));
        _coinText = FindComponent<Text>("Top/TopBar/Status_All/StatusGold/Text");
    }

    protected override void AddEvent()
    {
        base.AddEvent();
        RegisterButtonEvent(_closeBtn, OnClickCloseBtn);
    }

    public override void Show()
    {
        base.Show();
        if (SkillManager.Instance.SkillRoll.isUnlocked)
        {
            _skill_RollView.Unlock();
        }

        if (SkillManager.Instance.SkillClone.isUnlocked)
        {
            _skill_CloneView.Unlock();
        }

        if (SkillManager.Instance.SkillIdleBlock.isUnlocked)
        {
            _skill_IdleBlockView.Unlock();
        }

        _coinText.text = _dataProvider.Coin.ToString();
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