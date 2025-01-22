using TMPro;

public class GameView : UIBehaviour
{
    private TextMeshProUGUI _currencyTMP;

    protected override void ParseComponent()
    {
        _currencyTMP = FindComponent<TextMeshProUGUI>("");
    }

    public override void Dispose()
    {
    }
}