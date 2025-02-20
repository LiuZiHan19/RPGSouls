using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    private float sfxVolume;
    private float musicVolume;
    private Transform _sfxParent;
    private Transform _bgmParent;
    private AudioSource _bgmAudioSource;
    private List<AudioSource> _sfxAudioSources;

    /// <summary>
    /// 在编辑器状态下创建音频对象父对象进行管理
    /// </summary>
    public override void Initialize()
    {
        base.Initialize();
#if UNITY_EDITOR
        _sfxParent = new GameObject().transform;
        _bgmParent = new GameObject().transform;
        _sfxParent.name = "SfxGroup";
        _bgmParent.name = "BgmGroup";
        GameObject.DontDestroyOnLoad(_sfxParent);
        GameObject.DontDestroyOnLoad(_bgmParent);
#endif
    }

    public void SetSfxVolume(float volume)
    {
        sfxVolume = volume;
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
    }

    public void UpdateSfxVolume(float volume)
    {
        sfxVolume = volume;
        for (int i = 0; i < _sfxAudioSources.Count; i++)
        {
            _sfxAudioSources[i].volume = sfxVolume;
        }
    }

    public void UpdateMusicVolume(float volume)
    {
        musicVolume = volume;
        _bgmAudioSource.volume = musicVolume;
    }

    public void PlayBgm(string path, bool isLoop = true)
    {
        if (_bgmAudioSource == null)
        {
            _bgmAudioSource = AudioPool.Instance.Get();
#if UNITY_EDITOR
            _bgmAudioSource.gameObject.name = "BgmObj";
            _bgmAudioSource.gameObject.transform.SetParent(_bgmParent, false);
#endif
        }

        _bgmAudioSource.volume = musicVolume;
        AudioClip audioClip = ResourceLoader.Instance.LoadFromResources<AudioClip>(path);
        _bgmAudioSource.clip = audioClip;
        _bgmAudioSource.loop = isLoop;
        _bgmAudioSource.Play();
    }

    public void StopBgm()
    {
        if (_bgmAudioSource == null)
        {
            Debugger.Error("Audio Source is null");
            return;
        }

        _bgmAudioSource.Pause();
    }

    public void PlaySfx(string path, bool isLoop = false)
    {
        AudioSource audioSource = AudioPool.Instance.Get();
        audioSource.volume = sfxVolume;
        audioSource.clip = ResourceLoader.Instance.LoadFromResources<AudioClip>(path);
        audioSource.loop = isLoop;
        audioSource.Play();
        CoroutineManager.Instance.StartCor(WaitForAudioToEnd(audioSource.gameObject, audioSource));

#if UNITY_EDITOR
        audioSource.gameObject.name = "SfxObj";
        audioSource.gameObject.transform.SetParent(_sfxParent, false);
#endif
    }

    public void PlaySfx(string path, Vector3 selfPos, Vector3 targetPos, float distance = 10, bool isLoop = false)
    {
        if (Vector2.Distance(selfPos, targetPos) > distance)
        {
            Debugger.Info("Audio Source is out of range");
            return;
        }

        AudioSource audioSource = AudioPool.Instance.Get();
        audioSource.clip = ResourceLoader.Instance.LoadFromResources<AudioClip>(path);
        audioSource.loop = isLoop;
        audioSource.Play();
        CoroutineManager.Instance.StartCor(WaitForAudioToEnd(audioSource.gameObject, audioSource));

#if UNITY_EDITOR
        audioSource.gameObject.name = "SfxObj";
        audioSource.gameObject.transform.SetParent(_sfxParent, false);
#endif
    }

    private IEnumerator WaitForAudioToEnd(GameObject audioObj, AudioSource audioSource)
    {
        yield return new WaitUntil(() => !audioSource.isPlaying);
        AudioPool.Instance.Set(audioObj);
    }
}