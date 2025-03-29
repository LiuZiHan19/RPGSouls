using System;
using System.Collections;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private Color _igniteColor = Color.red;
    private Color _chillColor = Color.blue;
    private Color _lightingColor = Color.yellow;
    private SpriteRenderer _sr;
    private Color _oriColor;
    private Coroutine _currentCoroutine;

    protected virtual void Awake()
    {
        _sr = GetComponentInChildren<SpriteRenderer>();
        _oriColor = _sr.color;
    }

    protected virtual void Start()
    {
    }

    public void StartPlayElementStatusFx(ElementStatusType elementStatus)
    {
        StopElementStatusFX();

        switch (elementStatus)
        {
            case ElementStatusType.Ignite:
                _currentCoroutine = CoroutineManager.Instance.StartCoroutine(PlayElementStatusFx(elementStatus));
                break;
            case ElementStatusType.Chill:
                _currentCoroutine = CoroutineManager.Instance.StartCoroutine(PlayElementStatusFx(elementStatus));
                break;
            case ElementStatusType.Lighting:
                _currentCoroutine = CoroutineManager.Instance.StartCoroutine(PlayElementStatusFx(elementStatus));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(elementStatus), elementStatus, null);
        }
    }

    public void StopElementStatusFX()
    {
        _sr.color = _oriColor;
        if (_currentCoroutine != null) CoroutineManager.Instance.StopCoroutine(_currentCoroutine);
    }

    private IEnumerator PlayElementStatusFx(ElementStatusType elementStatus)
    {
        switch (elementStatus)
        {
            case ElementStatusType.Ignite:
                while (true)
                {
                    _sr.color = _igniteColor;
                    yield return new WaitForSeconds(0.1f);
                    _sr.color = _oriColor;
                    yield return new WaitForSeconds(0.1f);
                }
            case ElementStatusType.Chill:
                while (true)
                {
                    _sr.color = _chillColor;
                    yield return new WaitForSeconds(0.1f);
                    _sr.color = _oriColor;
                    yield return new WaitForSeconds(0.1f);
                }
            case ElementStatusType.Lighting:
                while (true)
                {
                    _sr.color = _lightingColor;
                    yield return new WaitForSeconds(0.1f);
                    _sr.color = _oriColor;
                    yield return new WaitForSeconds(0.1f);
                }
            default:
                throw new ArgumentOutOfRangeException(nameof(elementStatus), elementStatus, null);
        }
    }
}