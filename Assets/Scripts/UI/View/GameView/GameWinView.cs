using UnityEngine.UI;

public class GameWinView : UIBehaviour
{
    private Button _playAgainBtn;
    private Button _returnBtn;
    private Button _saveBtn;
    private IDataProvider _dataProvider;

    public GameWinView(IDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    protected override void ParseComponent()
    {
        _playAgainBtn = FindComponent<Button>("Middle/PlayAgain");
        _returnBtn = FindComponent<Button>("Middle/Return");
        _saveBtn = FindComponent<Button>("Middle/Save");
    }

    protected override void AddEvent()
    {
        base.AddEvent();
        RegisterButtonEvent(_playAgainBtn, OnClickPlayAgainBtn);
        RegisterButtonEvent(_returnBtn, OnClickReturnBtn);
        RegisterButtonEvent(_saveBtn, OnClickSaveBtn);
    }

    private void OnClickReturnBtn()
    {
        Hide();
        EventSubscriber.FromGameSceneToMenuScene?.Invoke();
    }

    private void OnClickPlayAgainBtn()
    {
        EventSubscriber.ReloadGameScene?.Invoke();
    }

    private void OnClickSaveBtn()
    {
        _dataProvider.SaveGameData();
        GameManager.Instance.ReloadData = true;
    }

    protected override void RemoveEvent()
    {
        base.RemoveEvent();
        UnRegisterButtonEvent(_playAgainBtn, OnClickPlayAgainBtn);
        UnRegisterButtonEvent(_returnBtn, OnClickReturnBtn);
        UnRegisterButtonEvent(_saveBtn, OnClickSaveBtn);
    }
}