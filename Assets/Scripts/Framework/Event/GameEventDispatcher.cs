using System;
using UnityEngine;

public class GameEventDispatcher
{
    public static Action OnClickPlayBtn
    {
        get;
        set;
    }

    public static Action OnPlayerDead
    {
        get; 
        set;
    }

    public static Action OnClickPlayAgainBtn
    {
        get; 
        set;
    }

    public static Action OnClickReturnBtn
    {
        get; 
        set;
    }

    public static Action<InventoryItemBaseSO> OnInventoryRealItemPickup
    {
        get; 
        set;
    }

    public static Action<InventoryItemBaseSO> Equip
    {
        get;
        set;
    }

    public static Action<InventoryItemBaseSO> UnEquip
    {
        get; 
        set;
    }

    public static Action<Transform> PlayerAttack
    {
        get;
        set;
    }
    
    public static Action<float> OnPlayerTakeDamage
    {
        get;
        set;
    }
}