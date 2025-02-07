using UnityEngine.UI;

public class MainMenuView : UIBehaviour
{
    private Button _playBtn;

    protected override void ParseComponent()
    {
        _playBtn = FindComponent<Button>("Play");
    }

    protected override void AddEvent()
    {
        base.AddEvent();
        RegisterButtonEvent(_playBtn, OnClickPlayBtn);
    }

    private void OnClickPlayBtn()
    {
        NotifyViewEvent(UIViewEventConst.MainMenuView.OnClickPlayBtn);
    }

    protected override void RemoveEvent()
    {
        base.RemoveEvent();
        UnregisterButtonEvent(_playBtn, OnClickPlayBtn);
    }
}