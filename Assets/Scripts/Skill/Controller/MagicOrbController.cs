using UnityEngine;

public class MagicOrbController : MonoBehaviour
{
    [SerializeField] private float orbitSpeed = 360f;
    [SerializeField] private float orbitRadius = 2f;

    private Transform _player;
    private float _currentAngle;

    public void Setup(float orbitSpeed = 360f, float orbitRadius = 2f)
    {
        this.orbitSpeed = orbitSpeed;
        this.orbitRadius = orbitRadius;
    }

    public void Release()
    {
        _player = PlayerManager.Instance.player.transform;
        Vector3 initialDirection = transform.position - _player.position + Vector3.up * 1.25f;
        _currentAngle = Mathf.Atan2(initialDirection.y, initialDirection.x) * Mathf.Rad2Deg;
    }

    void Update()
    {
        if (_player == null) return;

        _currentAngle += orbitSpeed * Time.deltaTime;

        float radians = _currentAngle * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(
            Mathf.Cos(radians) * orbitRadius,
            Mathf.Sin(radians) * orbitRadius + 1.25f,
            0
        );

        transform.position = _player.position + offset;
        transform.up = offset.normalized;
    }
}