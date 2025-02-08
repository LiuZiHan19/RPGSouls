using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public Player player;

    public override void Initialize()
    {
        base.Initialize();
        player = GameObject.FindObjectOfType<Player>();
    }
}