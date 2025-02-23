using UnityEngine;

public class PlayerFX : EntityFX
{
    private Player _player;

    protected override void Awake()
    {
        base.Awake();
        _player = GetComponent<Player>();
        EventDispatcher.PlayerAttack += PlayAttackFX;
    }

    public void PlayAttackFX()
    {
        if (InventoryManager.Instance.currentWeapon == null) return;

        if (InventoryManager.Instance.currentWeapon.equipmentType == E_InventoryEquipment.FlameSword)
        {
            PlayFlameFX();
        }
        else if (InventoryManager.Instance.currentWeapon.equipmentType == E_InventoryEquipment.IceSword)
        {
            PlayIceFX();
        }
        else if (InventoryManager.Instance.currentWeapon.equipmentType == E_InventoryEquipment.ThunderClaw)
        {
            PlayLightingFX();
        }
    }

    public void PlayFlameFX()
    {
        if (_player.facingDir == 1)
        {
            ResourceLoader.Instance.LoadObjFromResources("FX/FlameFX",
                _player.attackPoint.position + Vector3.up * 2 + Vector3.right * _player.facingDir,
                Quaternion.identity);
        }
        else
        {
            ResourceLoader.Instance.LoadObjFromResources("FX/FlameFX",
                _player.attackPoint.position + Vector3.up * 2 + Vector3.right * _player.facingDir,
                Quaternion.Euler(0, 180, 0));
        }
    }

    public void PlayIceFX()
    {
        if (_player.facingDir == 1)
        {
            ResourceLoader.Instance.LoadObjFromResources("FX/IceFX",
                _player.attackPoint.position + Vector3.right * _player.facingDir,
                Quaternion.identity);
        }
        else
        {
            ResourceLoader.Instance.LoadObjFromResources("FX/IceFX",
                _player.attackPoint.position + Vector3.right * _player.facingDir,
                Quaternion.Euler(0, 180, 0));
        }
    }

    public void PlayLightingFX()
    {
        if (_player.facingDir == 1)
        {
            ResourceLoader.Instance.LoadObjFromResources("FX/ThunderFX",
                _player.attackPoint.position + Vector3.right * _player.facingDir,
                Quaternion.identity);
        }
        else
        {
            ResourceLoader.Instance.LoadObjFromResources("FX/ThunderFX",
                _player.attackPoint.position + Vector3.right * _player.facingDir,
                Quaternion.Euler(0, 180, 0));
        }
    }
}