using UnityEngine;

public class UnityObjectHelper : Singleton<UnityObjectHelper>
{
    public void SetParent(Transform child, Transform parent, bool isStay = false)
    {
        child.SetParent(parent, isStay);
    }
}