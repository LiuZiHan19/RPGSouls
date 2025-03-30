using System;
using UnityEngine.UI;

public class SkillItemView : UIBehaviour
{
    public SkillID SkillID { get; set; }
    private Button _btn;
    private bool isUnlocked;
    private Image _lockImage;
    private IDataProvider _dataProvider;

    public SkillItemView(IDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    protected override void ParseComponent()
    {
        _btn = DisplayObject.GetComponent<Button>();
        _lockImage = FindComponent<Image>("Lock");
    }

    protected override void AddEvent()
    {
        base.AddEvent();
        RegisterButtonEvent(_btn, OnClickBtn);
    }

    public void Unlock()
    {
        _lockImage.fillAmount = 0;
    }

    private void OnClickBtn()
    {
        switch (SkillID)
        {
            case SkillID.Roll:
                if (SkillManager.Instance.SkillRoll.isUnlocked) return;
                if (SkillManager.Instance.CanUnlockSkill(SkillID) == false) return;
                if (_dataProvider.Coin < SkillManager.Instance.SkillRoll.price) return;
                _dataProvider.Coin -= SkillManager.Instance.SkillRoll.price;
                SkillManager.Instance.SkillRoll.isUnlocked = true;
                Unlock();

                break;
            case SkillID.IdleBlock:
                break;
            case SkillID.Clone:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    protected override void RemoveEvent()
    {
        base.RemoveEvent();
        UnRegisterButtonEvent(_btn, OnClickBtn);
    }
}