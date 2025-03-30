using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager m_instance;
    public static SoundManager Instance => m_instance;

#if UNITY_EDITOR
    private Transform m_sfxParent;
    private Transform m_bgmParent;
#endif

    private uint m_bgmID = 0;
    private float m_musicVolume = 1;
    private GameObject m_bgmHolder;
    private Dictionary<uint, AudioSource> m_bgmDict = new Dictionary<uint, AudioSource>();

    private ulong m_sfxID = 0;
    private float m_sfxVolume = 1;
    private Dictionary<ulong, AudioSource> m_sfxDict = new Dictionary<ulong, AudioSource>();

    private void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void InitialiseHierarchyWindow()
    {
#if UNITY_EDITOR
        m_sfxParent = new GameObject().transform;
        m_bgmParent = new GameObject().transform;
        m_sfxParent.name = "SfxGroup";
        m_bgmParent.name = "BgmGroup";
        DontDestroyOnLoad(m_sfxParent);
        DontDestroyOnLoad(m_bgmParent);
#endif
    }

    public void UpdateSfxVolume(float volume)
    {
        m_sfxVolume = volume;
        foreach (var sfx in m_sfxDict)
        {
            sfx.Value.volume = m_sfxVolume;
        }
    }

    public void UpdateMusicVolume(float volume)
    {
        m_musicVolume = volume;
        foreach (var bgm in m_bgmDict)
        {
            bgm.Value.volume = m_musicVolume;
        }
    }

    public void PlayBGM(AudioID audioID, ref uint id, bool isLoop = true)
    {
        if (m_bgmDict.ContainsKey(id))
        {
            m_bgmDict[id].loop = isLoop;
            m_bgmDict[id].Play();
            return;
        }

        if (m_bgmHolder == null)
        {
            m_bgmHolder = new GameObject();
            DontDestroyOnLoad(m_bgmHolder);
#if UNITY_EDITOR
            m_bgmHolder.transform.SetParent(m_bgmParent);
#endif
        }
#if UNITY_EDITOR
        m_bgmHolder.name = audioID.ToString();
#endif
        AudioSource audioSource = m_bgmHolder.AddComponent<AudioSource>();
        audioSource.clip = GameResources.Instance.AudioDataManifest.audioDataList.Find(x => x.AudioID == audioID)
            .audioClip;
        audioSource.loop = isLoop;
        audioSource.volume = m_musicVolume;
        audioSource.Play();
        m_bgmID++;
        m_bgmDict.Add(m_bgmID, audioSource);
        id = m_bgmID;
    }

    public void PlaySfx(AudioID audioID, ref ulong id, bool isLoop = false)
    {
        if (m_sfxDict.ContainsKey(id))
        {
            m_sfxDict[id].loop = isLoop;
            m_sfxDict[id].Play();
            return;
        }

        GameObject sfxHolder = new GameObject();
#if UNITY_EDITOR
        sfxHolder.name = audioID.ToString();
        sfxHolder.transform.SetParent(m_sfxParent);
#endif
        DontDestroyOnLoad(sfxHolder);
        AudioSource audioSource = sfxHolder.AddComponent<AudioSource>();
        audioSource.clip = GameResources.Instance.AudioDataManifest.audioDataList.Find(x => x.AudioID == audioID)
            .audioClip;
        audioSource.loop = isLoop;
        audioSource.volume = m_sfxVolume;
        audioSource.Play();
        m_sfxID++;
        m_sfxDict.Add(m_sfxID, audioSource);
        id = m_sfxID;
    }

    public void StopBGM(uint id)
    {
        if (!m_bgmDict.Keys.Contains(id))
        {
            Debugger.Warning($"[BGM not found in {nameof(StopBGM)} | Class: {GetType().Name}");
            return;
        }

        m_bgmDict[id].Stop();
    }

    public void StopSfx(ulong id)
    {
        if (!m_sfxDict.Keys.Contains(id))
        {
            Debugger.Warning($"[Sfx not found in {nameof(StopSfx)} | Class: {GetType().Name}");
            return;
        }

        m_sfxDict[id].Stop();
    }
}

public enum AudioID
{
    MenuBGM,
    GameBGM,
    BossBGM,
    AttackSfx1,
    AttackSfx2,
    AttackSfx3,
    EvilVoiceSfx,
    PlayerDeathSfx,
    PlayerStepSfx
}