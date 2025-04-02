using UnityEngine.UI;

public class DeadView : UIBehaviour
{
    private Button _playAgainBtn;
    private Button _returnBtn;

    protected override void ParseComponent()
    {
        _playAgainBtn = FindComponent<Button>("Middle/PlayAgain");
        _returnBtn = FindComponent<Button>("Middle/Return");
    }

    protected override void AddEvent()
    {
        base.AddEvent();
        RegisterButtonEvent(_playAgainBtn, OnClickPlayAgainBtn);
        RegisterButtonEvent(_returnBtn, OnClickReturnBtn);
    }

    private void OnClickReturnBtn()
    {
        Hide();
        EventSubscriber.FromGameSceneToMenuScene?.Invoke();
    }

    private void OnClickPlayAgainBtn()
    {
        Hide();
        GameManager.Instance.ResetPlayerHealth = true;
        EventSubscriber.ReloadGameScene?.Invoke();
    }

    public override void Dispose()
    {
        UnRegisterButtonEvent(_playAgainBtn, OnClickPlayAgainBtn);
        UnRegisterButtonEvent(_returnBtn, OnClickReturnBtn);
        base.Dispose();
    }
}