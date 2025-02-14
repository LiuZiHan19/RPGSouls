using System;

public class EventDispatcher
{
    public static Action OnClickPlay
    {
        get;
        set;
    }

    public static Action OnPlayerDead
    {
        get;
        set;
    }
    
    public static Action OnClickPlayAgain
    {
        get;
        set;
    }
    
    public static Action OnClickReturn
    {
        get;
        set;
    }
}