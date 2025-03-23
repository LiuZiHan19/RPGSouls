using System;
using System.Collections.Generic;
using UnityEngine;

public class FXPool : Singleton<FXPool>, IDisposable
{
    private Stack<ElementAttackFX> igniteFxPool = new Stack<ElementAttackFX>();
    private Stack<ElementAttackFX> chillFxPool = new Stack<ElementAttackFX>();
    private Stack<ElementAttackFX> lightingFxPool = new Stack<ElementAttackFX>();

    public ElementAttackFX GetFx(E_MagicStatus magicStatus, Transform transform, Vector3 euler = new Vector3())
    {
        switch (magicStatus)
        {
            case E_MagicStatus.Ignite:
                if (igniteFxPool.Count > 0) return GetPoolFx(igniteFxPool, transform, euler);
                return CreateFx("FX/IgniteFX", transform, euler);
            case E_MagicStatus.Chill:
                if (chillFxPool.Count > 0) return GetPoolFx(chillFxPool, transform, euler);
                return CreateFx("FX/ChillFX", transform, euler);
            case E_MagicStatus.Lighting:
                if (lightingFxPool.Count > 0) return GetPoolFx(lightingFxPool, transform, euler);
                return CreateFx("FX/LightingFX", transform, euler);
            default:
                throw new ArgumentOutOfRangeException(nameof(magicStatus), magicStatus, null);
        }
    }

    private ElementAttackFX GetPoolFx(Stack<ElementAttackFX> pool, Transform transform, Vector3 euler)
    {
        Player player = PlayerManager.Instance.player;

        ElementAttackFX elementAttackFX = pool.Pop();

        elementAttackFX.gameObject.transform.position = transform.position + Vector3.right * player.facingDir;

        if (elementAttackFX.fxType == E_MagicStatus.Ignite)
            elementAttackFX.gameObject.transform.position += Vector3.up * 2f;

        elementAttackFX.gameObject.transform.rotation = Quaternion.Euler(euler);

        return elementAttackFX;
    }

    private ElementAttackFX CreateFx(string path, Transform transform, Vector3 euler)
    {
        Player player = PlayerManager.Instance.player;

        ElementAttackFX elementAttackFX = ResourceLoader.Instance.LoadObjFromResources(
            path, transform.position + Vector3.right * player.facingDir,
            Quaternion.Euler(euler)
        ).GetComponent<ElementAttackFX>();

        if (elementAttackFX.fxType == E_MagicStatus.Ignite)
            elementAttackFX.gameObject.transform.position += Vector3.up * 2f;

        return elementAttackFX;
    }

    public void ReturnFx(ElementAttackFX elementAttackFX)
    {
        switch (elementAttackFX.fxType)
        {
            case E_MagicStatus.Ignite:
                igniteFxPool.Push(elementAttackFX);
                break;
            case E_MagicStatus.Chill:
                chillFxPool.Push(elementAttackFX);
                break;
            case E_MagicStatus.Lighting:
                lightingFxPool.Push(elementAttackFX);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void Dispose()
    {
        foreach (var fx in igniteFxPool)
        {
            GameObject.Destroy(fx.gameObject);
        }

        foreach (var fx in chillFxPool)
        {
            GameObject.Destroy(fx.gameObject);
        }

        foreach (var fx in lightingFxPool)
        {
            GameObject.Destroy(fx.gameObject);
        }

        igniteFxPool.Clear();
        chillFxPool.Clear();
        lightingFxPool.Clear();
    }
}