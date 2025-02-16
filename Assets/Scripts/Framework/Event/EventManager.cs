using System.Collections.Generic;
using UnityEngine.Events;

public class EventManager : Singleton<EventManager>
{
    public Dictionary<string, UnityAction> evtDict;

    public Dictionary<string, UnityAction<object>> evtArgDict;

    public Dictionary<string, UnityAction<object[]>> evtArgsDict;

    public override void Initialize()
    {
        base.Initialize();
        evtDict = new Dictionary<string, UnityAction>();
        evtArgDict = new Dictionary<string, UnityAction<object>>();
        evtArgsDict = new Dictionary<string, UnityAction<object[]>>();
    }

    public void AddEvent(string eventName, UnityAction<object> action)
    {
        if (evtArgDict.ContainsKey(eventName))
        {
            evtArgDict[eventName] += action;
        }
        else
        {
            evtArgDict.Add(eventName, action);
        }
    }

    public void RemoveEvent(string eventName, UnityAction<object> action)
    {
        if (evtArgDict.ContainsKey(eventName))
        {
            evtArgDict[eventName] -= action;
        }
    }

    public void TriggerEvent(string eventName, object args)
    {
        if (evtArgDict.ContainsKey(eventName))
        {
            evtArgDict[eventName](args);
        }
    }

    public void AddEvent(string eventName, UnityAction action)
    {
        if (evtDict.ContainsKey(eventName))
        {
            evtDict[eventName] += action;
        }
        else
        {
            evtDict.Add(eventName, action);
        }
    }

    public void RemoveEvent(string eventName, UnityAction action)
    {
        if (evtDict.ContainsKey(eventName))
        {
            evtDict[eventName] -= action;
        }
    }

    public void TriggerEvent(string eventName)
    {
        if (evtDict.ContainsKey(eventName))
        {
            evtDict[eventName]();
        }
    }

    public void AddEvent(string eventName, UnityAction<object[]> action)
    {
        if (evtArgsDict.ContainsKey(eventName))
        {
            evtArgsDict[eventName] += action;
        }
        else
        {
            evtArgsDict.Add(eventName, action);
        }
    }

    public void RemoveEvent(string eventName, UnityAction<object[]> action)
    {
        if (evtArgsDict.ContainsKey(eventName))
        {
            evtArgsDict[eventName] -= action;
        }
    }

    public void TriggerEvent(string eventName, object[] args)
    {
        if (evtArgsDict.ContainsKey(eventName))
        {
            evtArgsDict[eventName](args);
        }
    }
}