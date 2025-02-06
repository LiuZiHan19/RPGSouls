using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class UIBehaviour : IDisposable
{
    public GameObject Object;
    public Transform Transform;
    public RectTransform RectTransform;

    public void SetObject(GameObject obj)
    {
        Object = obj;
        Transform = obj.transform;
        RectTransform = obj.GetComponent<RectTransform>();
        ParseComponent();
        AddEvent();
    }

    protected T FindComponent<T>(string path)
    {
        T t = Transform.Find(path).GetComponent<T>();
        if (t == null)
            Logger.Error($"[UIBehaviour] Failed to find component of type '{typeof(T).Name}' at path '{path}'");
        return t;
    }

    protected void RegisterButtonEvent(Button button, UnityAction action)
    {
        button.onClick.AddListener(action);
    }

    protected void UnregisterButtonEvent(Button button, UnityAction action)
    {
        button.onClick.RemoveListener(action);
    }

    protected abstract void ParseComponent();

    protected virtual void AddEvent()
    {
    }

    protected virtual void RemoveEvent()
    {
    }

    public virtual void Dispose()
    {
        RemoveEvent();
    }
}