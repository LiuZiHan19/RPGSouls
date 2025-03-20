using System;
using UnityEngine.UI;

public class SkillItemView : UIBehaviour
{
    public SkillID SkillID { get; set; }
    private Button _btn;
    private bool isUnlocked;
    private Image _lockImage;

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
                if (GameDataManager.Instance.CanUnlockSkill(SkillID) == false) return;
                if (GameDataManager.Instance.SkillDataModel.skillRollData.isUlocked) return;
                if (GameDataManager.Instance.PlayerDataModel.coin > SkillManager.Instance.skillRoll.price)
                {
                    GameDataManager.Instance.PlayerDataModel.coin -= SkillManager.Instance.skillRoll.price;
                    GameDataManager.Instance.SkillDataModel.skillRollData.isUlocked = true;
                    SkillManager.Instance.skillRoll.isUnlocked = true;
                    Unlock();
                }

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

public enum SkillID
{
    Roll,
    IdleBlock,
    Clone
}