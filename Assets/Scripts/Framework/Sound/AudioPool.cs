using System.Collections.Generic;
using UnityEngine;

public class AudioPool : Singleton<AudioPool>
{
    private Queue<GameObject> _audioPool;

    public override void Initialize()
    {
        base.Initialize();
        _audioPool = new Queue<GameObject>();
    }

    public AudioSource Get()
    {
        if (_audioPool.Count > 0)
        {
            return _audioPool.Dequeue().GetComponent<AudioSource>();
        }

        GameObject obj = new GameObject();
        GameObject.DontDestroyOnLoad(obj);
        AudioSource audioSource = obj.AddComponent<AudioSource>();
        return audioSource;
    }

    public void Set(GameObject gameObject)
    {
        _audioPool.Enqueue(gameObject);
    }

    public void Dispose()
    {
        foreach (var audio in _audioPool)
        {
            GameObject.Destroy(audio);
        }
    }
}