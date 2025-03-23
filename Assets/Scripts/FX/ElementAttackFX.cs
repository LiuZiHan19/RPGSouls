using System.Collections;
using UnityEngine;

public class ElementAttackFX : MonoBehaviour
{
    public float waitForReturnTime = 1;
    public E_MagicStatus fxType;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void PlayFX()
    {
        _animator.SetBool("Fx", true);
        CoroutineManager.Instance.IStartCoroutine(WaitForReturn());
    }

    private IEnumerator WaitForReturn()
    {
        yield return new WaitForSeconds(waitForReturnTime);
        _animator.SetBool("Fx", false);
        FXPool.Instance.ReturnFx(this);
    }
}