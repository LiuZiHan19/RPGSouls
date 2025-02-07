using System.Collections;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    private Transform _sfxParent;
    private Transform _bgmParent;

    public override void Initialize()
    {
        base.Initialize();
        _sfxParent = new GameObject().transform;
        _bgmParent = new GameObject().transform;
#if UNITY_EDITOR
        _sfxParent.name = "Sfx";
        _bgmParent.name = "Bgm";
#endif
    }

    public void PlayBgm(string path, bool isLoop = true)
    {
        GameObject bgmObj = AudioPool.Instance.Get();
        AudioClip audioClip = ResourceLoader.Instance.LoadFromResources<AudioClip>(path);
        AudioSource audioSource = bgmObj.GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.loop = isLoop;
        audioSource.Play();
        CoroutineManager.Instance.StartCoroutine(WaitForAudioToEnd(bgmObj, audioSource));

#if UNITY_EDITOR
        bgmObj.name = "BgmObj";
        bgmObj.transform.SetParent(_bgmParent, false);
#endif
    }

    public void PlayBgm(AudioClip audioClip, bool isLoop = true)
    {
        GameObject bgmObj = AudioPool.Instance.Get();
        AudioSource audioSource = bgmObj.GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.loop = isLoop;
        audioSource.Play();
        CoroutineManager.Instance.StartCoroutine(WaitForAudioToEnd(bgmObj, audioSource));

#if UNITY_EDITOR
        bgmObj.name = "BgmObj";
        bgmObj.transform.SetParent(_bgmParent, false);
#endif
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

    public void PlaySfx(AudioClip audioClip, bool isLoop = false)
    {
        GameObject sfxObj = AudioPool.Instance.Get();
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