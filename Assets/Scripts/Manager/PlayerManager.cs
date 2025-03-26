using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public Player player;
    public bool IsPlayerDead { get; set; }

    public override void Initialize()
    {
        base.Initialize();
        player = GameObject.FindObjectOfType<Player>();
        GameDataManager.Instance.PlayerDataModel.PareSelf(() =>
        {
            GameEventDispatcher.OnPlayerTakeDamage?.Invoke((float)player.playerStats.currentHealth /
                                                           player.playerStats.maxHealth.GetValue());
        });
    }
}