using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class UIBehaviour : IDisposable
{
    public GameObject UIObject;
    public Transform UITransform;
    public RectTransform UIRectTransform;
    public Action<string, object> UIEvent;

    public void SetObject(GameObject obj)
    {
        UIObject = obj;
        UITransform = obj.transform;
        UIRectTransform = obj.GetComponent<RectTransform>();
        ParseComponent();
        AddEvent();
    }

    public virtual void Show()
    {
        UIObject.SetActive(true);
    }

    public virtual void Hide()
    {
        UIObject.SetActive(false);
    }

    public void AddUIEvent(Action<string, object> uiEvent)
    {
        UIEvent += uiEvent;
    }

    public void RemoveUIEvent(Action<string, object> uiEvent)
    {
        UIEvent -= uiEvent;
    }

    protected void NotifyViewEvent(string evtType, object data = null)
    {
        UIEvent?.Invoke(evtType, data);
    }

    protected T FindComponent<T>(string path)
    {
        T t = UITransform.Find(path).GetComponent<T>();
        if (t == null)
            Debugger.Error($"[UIBehaviour] Failed to find component of type '{typeof(T).Name}' at path '{path}'");
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
        UnityEngine.Object.DestroyImmediate(UIObject);
        UIObject = null;
        UITransform = null;
        UIRectTransform = null;
        UIEvent = null;
    }
}