using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoroutineManager : MonoSingletonAuto<CoroutineManager>
{
    private List<Coroutine> _coroutines = new List<Coroutine>();

    public Coroutine IStartCoroutine(IEnumerator coroutine)
    {
        Coroutine cor = StartCoroutine(coroutine);
        _coroutines.Add(cor);
        return cor;
    }

    public void IStopCoroutine(Coroutine coroutine)
    {
        if (coroutine == null)
        {
            Debugger.Warning("Coroutine is null");
            return;
        }

        StopCoroutine(coroutine);
    }

    public void IStopAllCoroutine()
    {
        foreach (Coroutine cor in _coroutines)
        {
            IStopCoroutine(cor);
        }
    }

    public void WaitForSeconds(float seconds, UnityAction callback)
    {
        IStartCoroutine(WaitForSecondsCoroutine(seconds, callback));
    }

    private IEnumerator WaitForSecondsCoroutine(float seconds, UnityAction callback)
    {
        yield return new WaitForSeconds(seconds);
        callback?.Invoke();
    }
}