using UnityEngine;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManager.Instance.IsBossDead == false) return;

        if (other.gameObject.tag == "player")
        {
            GameEventDispatcher.OnGameWin?.Invoke();
        }
    }
}