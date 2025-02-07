using TMPro;

public class MainGameView : UIBehaviour
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