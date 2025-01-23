using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        AudioManager.Instance.Initialize();
        AudioPool.Instance.Initialize();
    }

    private void Start()
    {
    }
}