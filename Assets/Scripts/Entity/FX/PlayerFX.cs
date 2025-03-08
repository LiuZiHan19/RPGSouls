using UnityEngine;

public class PlayerFX : EntityFX
{
    private Player _player;

    protected override void Awake()
    {
        base.Awake();
        _player = GetComponent<Player>();
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
            FXPool.Instance.GetIgniteFx(transform).PlayFX();
        else
            FXPool.Instance.GetIgniteFx(transform, new Vector3(0, 180, 0)).PlayFX();
    }

    public void PlayChillFX(Transform transform)
    {
        if (_player.facingDir == 1)
            FXPool.Instance.GetChillFx(transform).PlayFX();
        else
            FXPool.Instance.GetChillFx(transform, new Vector3(0, 180, 0)).PlayFX();
    }

    public void PlayLightingFX(Transform transform)
    {
        if (_player.facingDir == 1)
            FXPool.Instance.GetLightingFx(transform).PlayFX();
        else
            FXPool.Instance.GetLightingFx(transform, new Vector3(0, 180, 0)).PlayFX();
    }
}