using UnityEngine.UI;

public class InventorySlotView : UIBehaviour
{
    private InventoryItemBaseSO _itemSO;
    private Image _sr;

    protected override void ParseComponent()
    {
        _sr = FindComponent<Image>("Pic");
    }

    public override void Refresh(params object[] args)
    {
        base.Refresh(args);
        if (_itemSO != null) EventDispatcher.UnEquip?.Invoke(_itemSO);

        _itemSO = args[0] as InventoryItemBaseSO;
        _sr.sprite = _itemSO.sprite;
    }
}