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

    public void PlayMagicStatusFX(E_MagicStatus magicStatus)
    {
        StopMagicStatusFX();

        switch (magicStatus)
        {
            case E_MagicStatus.Ignite:
                _currentCoroutine = CoroutineManager.Instance.IStartCoroutine(PlayMagicStatusFXCor(magicStatus));
                break;
            case E_MagicStatus.Chill:
                _currentCoroutine = CoroutineManager.Instance.IStartCoroutine(PlayMagicStatusFXCor(magicStatus));
                break;
            case E_MagicStatus.Lighting:
                _currentCoroutine = CoroutineManager.Instance.IStartCoroutine(PlayMagicStatusFXCor(magicStatus));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(magicStatus), magicStatus, null);
        }
    }

    public void StopMagicStatusFX()
    {
        _sr.color = _oriColor;
        CoroutineManager.Instance.IStopCoroutine(_currentCoroutine);
    }

    private IEnumerator PlayMagicStatusFXCor(E_MagicStatus magicStatus)
    {
        switch (magicStatus)
        {
            case E_MagicStatus.Ignite:
                while (true)
                {
                    _sr.color = _igniteColor;
                    yield return new WaitForSeconds(0.1f);
                    _sr.color = _oriColor;
                    yield return new WaitForSeconds(0.1f);
                }
            case E_MagicStatus.Chill:
                while (true)
                {
                    _sr.color = _chillColor;
                    yield return new WaitForSeconds(0.1f);
                    _sr.color = _oriColor;
                    yield return new WaitForSeconds(0.1f);
                }
            case E_MagicStatus.Lighting:
                while (true)
                {
                    _sr.color = _lightingColor;
                    yield return new WaitForSeconds(0.1f);
                    _sr.color = _oriColor;
                    yield return new WaitForSeconds(0.1f);
                }
            default:
                throw new ArgumentOutOfRangeException(nameof(magicStatus), magicStatus, null);
        }
    }
}