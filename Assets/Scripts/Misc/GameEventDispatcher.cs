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

    public static Action<InventoryItemBaseData> OnInventoryRealItemPickup
    {
        get; 
        set;
    }

    public static Action<InventoryItemBaseData> Equip
    {
        get;
        set;
    }

    public static Action<InventoryItemBaseData> UnEquip
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

    public static Action OnGameWin
    {
        get;
        set;
    }
}