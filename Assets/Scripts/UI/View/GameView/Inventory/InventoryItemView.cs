using UnityEngine.UI;

public class InventoryItemView : UIBehaviour
{
    public InventoryItemBaseSO itemSO;
    public Text numberText;
    private Image _iconImage;
    private Button _btn;

    protected override void ParseComponent()
    {
        _iconImage = FindComponent<Image>("Icon");
        numberText = FindComponent<Text>("Number");
        _btn = DisplayObject.GetComponent<Button>();
    }

    protected override void AddEvent()
    {
        base.AddEvent();
        RegisterButtonEvent(_btn, OnClickBtn);
    }

    public void Initialise(InventoryItemBaseSO itemSO, int number)
    {
        this.itemSO = itemSO;
        _iconImage.sprite = itemSO.sprite;
        numberText.text = number.ToString();
    }

    private void OnClickBtn()
    {
        if (itemSO.itemBaseType == E_InventoryItemBase.Equipment)
        {
            EventDispatcher.Equip?.Invoke(itemSO);
        }
    }

    protected override void RemoveEvent()
    {
        UnregisterButtonEvent(_btn, OnClickBtn);
        base.RemoveEvent();
    }
}