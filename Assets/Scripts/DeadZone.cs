using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "player")
        {
            EventDispatcher.OnPlayerDead?.Invoke();
        }
    }
}