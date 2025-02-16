using UnityEngine;

public class InventoryRealItem : MonoBehaviour
{
    public InventoryItemBaseSO itemData;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "player")
        {
            EventDispatcher.OnInventoryRealItemPickup?.Invoke(itemData);
            Destroy(gameObject);
        }
    }

    private void OnValidate()
    {
#if UNITY_EDITOR
        if (itemData != null) gameObject.name = itemData.name;
#endif
    }
}