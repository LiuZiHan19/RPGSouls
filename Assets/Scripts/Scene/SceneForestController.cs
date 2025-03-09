using UnityEngine;

public class SceneForestController : MonoBehaviour
{
    private Transform _playerSpawnPoint;

    private void Awake()
    {
        _playerSpawnPoint = GameObject.Find("PlayerSpawnPoint").transform;
    }

    private void Start()
    {
        ResourceLoader.Instance.LoadObjFromResources("Setting/MainCamera");
        ResourceLoader.Instance.LoadObjFromResources("Entity/Player", _playerSpawnPoint.position,
            Quaternion.identity);
    }
}