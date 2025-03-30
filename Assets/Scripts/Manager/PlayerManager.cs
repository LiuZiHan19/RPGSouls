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
        DataManager.Instance.PlayerDataModel.ParseJSONData(UpdateOnParseDataCompleted);
    }

    private void UpdateOnParseDataCompleted()
    {
        if (GameManager.Instance.ResetPlayerHealth)
        {
            GameManager.Instance.ResetPlayerHealth = false;
            player.playerStats.currentHealth = 200;
            EventDispatcher.OnPlayerHealthChange?.Invoke(1);
        }
        else
        {
            float percentage = (float)player.playerStats.currentHealth / player.playerStats.maxHealth.GetValue();
            EventDispatcher.OnPlayerHealthChange?.Invoke(percentage);
        }
    }
}