using System;
using System.Collections.Generic;
using UnityEngine;

public class FXPool : Singleton<FXPool>
{
    private Stack<AttackFXController> igniteFxPool = new Stack<AttackFXController>();
    private Stack<AttackFXController> chillFxPool = new Stack<AttackFXController>();
    private Stack<AttackFXController> lightingFxPool = new Stack<AttackFXController>();

    public AttackFXController GetFx(E_MagicStatus magicStatus, Transform transform, Vector3 euler = new Vector3())
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

    private AttackFXController GetPoolFx(Stack<AttackFXController> pool, Transform transform, Vector3 euler)
    {
        Player player = PlayerManager.Instance.player;

        AttackFXController attackFX = pool.Pop();

        attackFX.gameObject.transform.position = transform.position + Vector3.right * player.facingDir;

        if (attackFX.fxType == E_MagicStatus.Ignite) attackFX.gameObject.transform.position += Vector3.up * 2f;

        attackFX.gameObject.transform.rotation = Quaternion.Euler(euler);

        return attackFX;
    }

    private AttackFXController CreateFx(string path, Transform transform, Vector3 euler)
    {
        Player player = PlayerManager.Instance.player;

        AttackFXController attackFX = ResourceLoader.Instance.LoadObjFromResources(
            path, transform.position + Vector3.right * player.facingDir,
            Quaternion.Euler(euler)
        ).GetComponent<AttackFXController>();

        if (attackFX.fxType == E_MagicStatus.Ignite) attackFX.gameObject.transform.position += Vector3.up * 2f;

        return attackFX;
    }

    public void ReturnFx(AttackFXController attackFX)
    {
        switch (attackFX.fxType)
        {
            case E_MagicStatus.Ignite:
                igniteFxPool.Push(attackFX);
                break;
            case E_MagicStatus.Chill:
                chillFxPool.Push(attackFX);
                break;
            case E_MagicStatus.Lighting:
                lightingFxPool.Push(attackFX);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void Clear()
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