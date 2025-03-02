using System;
using System.Collections.Generic;
using UnityEngine;

public class FXPool : Singleton<FXPool>
{
    private Stack<AttackFXController> igniteFxPool = new Stack<AttackFXController>();
    private Stack<AttackFXController> chillFxPool = new Stack<AttackFXController>();
    private Stack<AttackFXController> lightingFxPool = new Stack<AttackFXController>();

    public AttackFXController GetIgniteFx(Transform transform, Vector3 euler = new Vector3())
    {
        ClearPool(chillFxPool, lightingFxPool);

        if (igniteFxPool.Count > 0) return GetPooledFX(igniteFxPool, transform, euler);

        return CreateFX("FX/IgniteFX", transform, euler);
    }

    public AttackFXController GetChillFx(Transform transform, Vector3 euler = new Vector3())
    {
        ClearPool(igniteFxPool, lightingFxPool);

        if (chillFxPool.Count > 0) return GetPooledFX(chillFxPool, transform, euler);

        return CreateFX("FX/ChillFX", transform, euler);
    }

    public AttackFXController GetLightingFx(Transform transform, Vector3 euler = new Vector3())
    {
        ClearPool(igniteFxPool, chillFxPool);

        if (lightingFxPool.Count > 0) return GetPooledFX(lightingFxPool, transform, euler);

        return CreateFX("FX/LightingFX", transform, euler);
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

    private void ClearPool(params Stack<AttackFXController>[] pools)
    {
        foreach (var pool in pools)
        {
            while (pool.Count > 0) GameObject.Destroy(pool.Pop().gameObject);
        }
    }

    private AttackFXController GetPooledFX(Stack<AttackFXController> pool, Transform transform, Vector3 euler)
    {
        AttackFXController attackFX = pool.Pop();
        Player player = PlayerManager.Instance.player;

        attackFX.gameObject.transform.position = transform.position + Vector3.right * player.facingDir;

        if (attackFX.fxType == E_MagicStatus.Ignite) attackFX.gameObject.transform.position += Vector3.up * 2f;

        attackFX.gameObject.transform.rotation = Quaternion.Euler(euler);
        attackFX.gameObject.SetActive(true);
        return attackFX;
    }

    private AttackFXController CreateFX(string path, Transform transform, Vector3 euler)
    {
        Player player = PlayerManager.Instance.player;
        AttackFXController attackFX = ResourceLoader.Instance.LoadObjFromResources(
            path, transform.position + Vector3.right * player.facingDir,
            Quaternion.Euler(euler)
        ).GetComponent<AttackFXController>();

        if (attackFX.fxType == E_MagicStatus.Ignite) attackFX.gameObject.transform.position += Vector3.up * 2f;

        return attackFX;
    }
}