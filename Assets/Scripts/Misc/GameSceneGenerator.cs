using UnityEngine;

public class GameSceneGenerator : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject mainCameraPrefab;
    private Transform _playerSpawnPoint;

    private void Awake()
    {
        _playerSpawnPoint = GameObject.Find("PlayerSpawnPoint").transform;
    }

    private void Start()
    {
        Instantiate(mainCameraPrefab);
        Instantiate(playerPrefab, _playerSpawnPoint.position, Quaternion.identity);
    }
}