using UnityEngine;

public class ColliderChecker : MonoBehaviour
{
    public string layerName;
    private Collider _collider;
    private bool _isChecked;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    public bool IsChecked()
    {
        return _isChecked;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(layerName))
        {
            _isChecked = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(layerName))
        {
            _isChecked = false;
        }
    }
}