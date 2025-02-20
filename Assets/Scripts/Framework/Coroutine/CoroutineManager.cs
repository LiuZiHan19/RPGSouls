using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager : MonoSingletonAuto<CoroutineManager>
{
    private List<Coroutine> _coroutines = new List<Coroutine>();

    public Coroutine StartCor(IEnumerator coroutine)
    {
        Coroutine cor = StartCoroutine(coroutine);
        _coroutines.Add(cor);
        return cor;
    }

    public void StopCor(Coroutine coroutine)
    {
        StopCoroutine(coroutine);
    }

    public void StopAllCor()
    {
        foreach (var coroutine in _coroutines)
        {
            StopCor(coroutine);
        }
    }
}