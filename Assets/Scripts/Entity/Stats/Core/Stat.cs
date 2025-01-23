using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private int _baseValue;
    [SerializeField] private List<int> _modifiers;

    public int GetValue()
    {
        int finalValue = _baseValue;
        foreach (var modifier in _modifiers) finalValue += modifier;
        return finalValue;
    }

    public int SetDefaultValue(int value) => _baseValue = value;

    public void AddModifier(int modifier) => _modifiers.Add(modifier);

    public void RemoveModifier(int modifier) => _modifiers.RemoveAt(modifier);
}