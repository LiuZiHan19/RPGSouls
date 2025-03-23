using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    private bool _isTriggered;

    public bool IsTriggered
    {
        get
        {
            if (_isTriggered)
            {
                _isTriggered = false;
                return true;
            }

            return false;
        }
    }

    public void Trigger()
    {
        _isTriggered = true;
    }
}