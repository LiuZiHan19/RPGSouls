using UnityEngine;

public abstract class AnimEvent : MonoBehaviour
{
    private bool _isTrigged;

    protected virtual void Awake()
    {
    }

    protected virtual void Start()
    {
    }

    public void Trigger()
    {
        _isTrigged = true;
    }

    public bool IsTriggered()
    {
        bool isTrigger = _isTrigged;
        _isTrigged = false;
        return isTrigger;
    }
}