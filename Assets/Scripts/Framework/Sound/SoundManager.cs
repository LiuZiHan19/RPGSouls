using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    private float sfxVolume;
    private float musicVolume;
    private Transform _sfxParent;
    private Transform _bgmParent;
    private AudioSource _bgmAudioSource;
    private Dictionary<uint, AudioSource> _sfxAudioSources;
    private uint _signCounter;

    /// <summary>
    /// 在编辑器状态下创建音频对象父对象进行管理
    /// </summary>
    public override void Initialize()
    {
        _sfxAudioSources = new Dictionary<uint, AudioSource>();
        base.Initialize();
        sfxVolume = 1;
        musicVolume = 1;
#if UNITY_EDITOR
        _sfxParent = new GameObject().transform;
        _bgmParent = new GameObject().transform;
        _sfxParent.name = "SfxGroup";
        _bgmParent.name = "BgmGroup";
        GameObject.DontDestroyOnLoad(_sfxParent);
        GameObject.DontDestroyOnLoad(_bgmParent);
#endif
    }

    public void UpdateSfxVolume(float volume)
    {
        sfxVolume = volume;
        foreach (var kv in _sfxAudioSources)
        {
            kv.Value.volume = sfxVolume;
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
            _bgmAudioSource = SoundPool.Instance.Get();
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

    public uint PlaySfx(string path, bool isLoop = false)
    {
        AudioSource audioSource = SoundPool.Instance.Get();
        audioSource.volume = sfxVolume;
        audioSource.clip = ResourceLoader.Instance.LoadFromResources<AudioClip>(path);
        audioSource.loop = isLoop;
        audioSource.Play();
        CoroutineManager.Instance.StartCor(WaitForAudioToEnd(audioSource.gameObject, audioSource));

#if UNITY_EDITOR
        audioSource.gameObject.name = "SfxObj";
        audioSource.gameObject.transform.SetParent(_sfxParent, false);
#endif
        _signCounter++;
        _sfxAudioSources.Add(_signCounter, audioSource);
        return _signCounter;
    }

    public uint PlaySfx(string path, Vector3 selfPos, Vector3 targetPos, float distance = 10, bool isLoop = false)
    {
        if (Vector2.Distance(selfPos, targetPos) > distance)
        {
            Debugger.Info("Audio Source is out of range");
            return 0;
        }

        AudioSource audioSource = SoundPool.Instance.Get();
        audioSource.clip = ResourceLoader.Instance.LoadFromResources<AudioClip>(path);
        audioSource.loop = isLoop;
        audioSource.Play();
        CoroutineManager.Instance.StartCor(WaitForAudioToEnd(audioSource.gameObject, audioSource));

#if UNITY_EDITOR
        audioSource.gameObject.name = "SfxObj";
        audioSource.gameObject.transform.SetParent(_sfxParent, false);
#endif

        _signCounter++;
        _sfxAudioSources.Add(_signCounter, audioSource);
        return _signCounter;
    }

    public void StopSfx(uint sign)
    {
        if (_sfxAudioSources.ContainsKey(sign))
        {
            _sfxAudioSources[sign].Stop();
        }
    }

    private IEnumerator WaitForAudioToEnd(GameObject audioObj, AudioSource audioSource)
    {
        yield return new WaitUntil(() => !audioSource.isPlaying);
        SoundPool.Instance.Set(audioObj);
    }
}