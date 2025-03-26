using UnityEngine;

public class GameDataModel
{
    public float soundVolume;
    public float musicVolume;

    public void Save()
    {
        PlayerPrefs.SetFloat("SoundVolume", soundVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
    }

    public void Load()
    {
        soundVolume = PlayerPrefs.GetFloat("SoundVolume", 1f);
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        SoundManager.Instance.UpdateMusicVolume(musicVolume);
        SoundManager.Instance.UpdateSfxVolume(soundVolume);
    }
}