using UnityEngine;

public class MainGameViewController : UIController
{
    private MainGameView _mainGameView;
    private InventoryView _inventoryView;
    private SkillView _skillView;

    public override void Initialise()
    {
        _mainGameView = new MainGameView();
        GameObject obj = ResourceLoader.Instance.LoadObjFromResources("MainGameView");
        _mainGameView.SetObject(obj);
        UIManager.Instance.SetObjectToLayer(obj.transform, UILayer.Middle);
        _mainGameView.Show();
    }

    public override void Dispose()
    {
        _mainGameView.Dispose();
        _mainGameView = null;
    }
}