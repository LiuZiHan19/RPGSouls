using UnityEngine;

public class CheckCollider : MonoBehaviour
{
    public string layerName;
    private Collider _collider;
    private bool _isColliding;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    public bool IsColliding()
    {
        return _isColliding;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(layerName))
        {
            _isColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(layerName))
        {
            _isColliding = false;
        }
    }
}