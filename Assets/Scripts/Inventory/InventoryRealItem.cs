using UnityEngine;
using Random = UnityEngine.Random;

public class InventoryRealItem : MonoBehaviour
{
    public InventoryItemBaseSO itemData;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        Pop();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "player")
        {
            EventDispatcher.OnInventoryRealItemPickup?.Invoke(itemData);
            Destroy(gameObject);
        }
    }

    public void Pop()
    {
        _rb.velocity = new Vector2(Random.Range(-5f, 5f), Random.Range(16, 20f));
    }

    private void OnValidate()
    {
#if UNITY_EDITOR
        if (itemData != null) gameObject.name = itemData.name;
#endif
    }
}