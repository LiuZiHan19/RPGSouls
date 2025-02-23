using UnityEngine.UI;

public class InventoryStatView : UIBehaviour
{
    private Text _infoText;

    protected override void ParseComponent()
    {
        _infoText = FindComponent<Text>("Info");
    }

    public override void Refresh(params object[] args)
    {
        base.Refresh(args);
        string name = args[0] as string;
        int value = (int)args[1];
        _infoText.text = $"{name}: {value}";
    }
}