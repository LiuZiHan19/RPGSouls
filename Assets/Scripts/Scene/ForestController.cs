using UnityEngine;

public class ForestController : MonoBehaviour
{
    private Transform _playerSpawnPoint;

    private void Awake()
    {
        _playerSpawnPoint = transform.Find("PlayerSpawnPoint");
    }

    private void Start()
    {
        ResourceLoader.Instance.LoadObjFromResources("MainCamera");
        GameObject player = ResourceLoader.Instance.LoadObjFromResources("Entity/Player");
        player.transform.position = _playerSpawnPoint.position;
    }
}