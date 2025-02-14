using System.Collections;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    private Transform _sfxParent;
    private Transform _bgmParent;
    private AudioSource _bgmSource;

    public override void Initialize()
    {
        base.Initialize();
#if UNITY_EDITOR
        _sfxParent = new GameObject().transform;
        _bgmParent = new GameObject().transform;
        _sfxParent.name = "Sfx";
        _bgmParent.name = "Bgm";
        GameObject.DontDestroyOnLoad(_sfxParent);
        GameObject.DontDestroyOnLoad(_bgmParent);
#endif
    }

    public void PlayBgm(string path, bool isLoop = true)
    {
        if (_bgmSource == null)
        {
            GameObject bgmObj = AudioPool.Instance.Get();
            _bgmSource = bgmObj.GetComponent<AudioSource>();
            GameObject.DontDestroyOnLoad(bgmObj);
#if UNITY_EDITOR
            bgmObj.name = "BgmObj";
            bgmObj.transform.SetParent(_bgmParent, false);
#endif
        }

        AudioClip audioClip = ResourceLoader.Instance.LoadFromResources<AudioClip>(path);
        _bgmSource.clip = audioClip;
        _bgmSource.loop = isLoop;
        _bgmSource.Play();
    }

    public void StopBgm()
    {
        _bgmSource.Pause();
    }

    public void PlaySfx(string path, bool isLoop = false)
    {
        GameObject sfxObj = AudioPool.Instance.Get();
        AudioClip audioClip = ResourceLoader.Instance.LoadFromResources<AudioClip>(path);
        AudioSource audioSource = sfxObj.GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.loop = isLoop;
        audioSource.Play();
        CoroutineManager.Instance.StartCoroutine(WaitForAudioToEnd(sfxObj, audioSource));

#if UNITY_EDITOR
        sfxObj.name = "SfxObj";
        sfxObj.transform.SetParent(_sfxParent, false);
#endif
    }

    private IEnumerator WaitForAudioToEnd(GameObject audioObj, AudioSource audioSource)
    {
        yield return new WaitUntil(() => !audioSource.isPlaying);
        AudioPool.Instance.Set(audioObj);
    }
}