using UnityEngine;
using UnityEngine.Serialization;

public class InventoryItemBaseData : ScriptableObject
{
    public int id;
    public string name;
    public InventoryItemBaseType itemBaseType;
}