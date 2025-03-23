using UnityEngine;

public class PlayerFX : EntityFX
{
    private Player _player;

    protected override void Awake()
    {
        base.Awake();
        _player = GetComponent<Player>();
        GameEventDispatcher.PlayerAttack = null;
        GameEventDispatcher.PlayerAttack += PlayAttackFX;
    }

    public void PlayAttackFX(Transform transform)
    {
        if (InventoryManager.Instance.weapon == null) return;

        if (InventoryManager.Instance.weapon.equipmentType == E_InventoryEquipment.FlameSword)
            PlayIgniteFX(transform);
        else if (InventoryManager.Instance.weapon.equipmentType == E_InventoryEquipment.IceSword)
            PlayChillFX(transform);
        else if (InventoryManager.Instance.weapon.equipmentType == E_InventoryEquipment.ThunderClaw)
            PlayLightingFX(transform);
    }

    public void PlayIgniteFX(Transform transform)
    {
        if (_player.facingDir == 1)
            FXPool.Instance.GetFx(E_MagicStatus.Ignite, transform).PlayFX();
        else
            FXPool.Instance.GetFx(E_MagicStatus.Ignite, transform, new Vector3(0, 180, 0)).PlayFX();
    }

    public void PlayChillFX(Transform transform)
    {
        if (_player.facingDir == 1)
            FXPool.Instance.GetFx(E_MagicStatus.Chill, transform).PlayFX();
        else
            FXPool.Instance.GetFx(E_MagicStatus.Chill, transform, new Vector3(0, 180, 0)).PlayFX();
    }

    public void PlayLightingFX(Transform transform)
    {
        if (_player.facingDir == 1)
            FXPool.Instance.GetFx(E_MagicStatus.Lighting, transform).PlayFX();
        else
            FXPool.Instance.GetFx(E_MagicStatus.Lighting, transform, new Vector3(0, 180, 0)).PlayFX();
    }

    ~PlayerFX()
    {
        GameEventDispatcher.PlayerAttack -= PlayAttackFX;
    }
}