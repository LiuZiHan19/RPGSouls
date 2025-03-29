using UnityEngine;

public class PlayerManager
{
    private static PlayerManager m_instance;
    public static PlayerManager Instance => m_instance ?? (m_instance = new PlayerManager());

    public Player player;
    public bool IsPlayerDead { get; set; }

    public void Initialize()
    {
        player = GameObject.FindObjectOfType<Player>();
        GameDataManager.Instance.PlayerDataModel.PareSelf(() =>
        {
            GameEventDispatcher.OnPlayerTakeDamage?.Invoke((float)player.playerStats.currentHealth /
                                                           player.playerStats.maxHealth.GetValue());
        });
    }
}