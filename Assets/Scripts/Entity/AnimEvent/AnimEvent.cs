using System;
using UnityEngine;

public class AnimEvent : MonoBehaviour
{
    private bool _isTrigger;

    protected virtual void Awake()
    {
        
    }

    public void Trigger()
    {
        _isTrigger = true;
    }

    public bool IsTriggered()
    {
        bool isTrigger = _isTrigger;
        _isTrigger = false;
        return isTrigger;
    }
}