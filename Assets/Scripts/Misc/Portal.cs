using UnityEngine;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManager.Instance.IsGrimReaperDead == false) return;

        if (other.gameObject.tag == "player")
        {
            EventDispatcher.OnGameWin?.Invoke();
        }
    }
}