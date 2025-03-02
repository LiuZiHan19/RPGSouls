using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public Player player;

    public override void Initialize()
    {
        base.Initialize();
        player = GameObject.FindObjectOfType<Player>();
        Debugger.Info("lzh " + Application.persistentDataPath);
        JsonManager.Instance.LoadJsonDataAsync("PlayerData", value =>
        {
            if (value != null)
            {
                GameDataManager.Instance.ParsePlayerData(value);
            }
            else
            {
                Debugger.Warning("Load PlayerData Failed");
            }
        });
    }
}