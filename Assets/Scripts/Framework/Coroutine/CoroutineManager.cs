using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}