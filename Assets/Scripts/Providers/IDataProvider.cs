using System;
using UnityEngine.Events;

public interface IDataProvider
{
    public int Coin { get; set; }
    public float SoundVolume { get; set; }
    public float MusicVolume { get; set; }
    public void SaveGameData(UnityAction callback = null);
}