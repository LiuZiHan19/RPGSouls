using System.Collections.Generic;
using UnityEngine;

public class AudioPool : Singleton<AudioPool>
{
    private Queue<GameObject> _audioPool;

    public void Initialize()
    {
        _audioPool = new Queue<GameObject>();
    }

    public GameObject Get()
    {
        if (_audioPool.Count > 0)
        {
            return _audioPool.Dequeue();
        }

        GameObject obj = new GameObject();
        obj.AddComponent<AudioSource>();
        return obj;
    }

    public void Set(GameObject gameObject)
    {
        _audioPool.Enqueue(gameObject);
    }

    public void Dispose()
    {
        foreach (var audio in _audioPool) GameObject.Destroy(audio);
    }
}