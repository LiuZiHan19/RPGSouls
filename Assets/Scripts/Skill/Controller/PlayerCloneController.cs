using System;
using UnityEngine;

public class PlayerCloneController : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _sr;
    private PlayerAnimEvent _playerAnimEvent;
    private bool _isFade;
    private float _fadeSpeed = 4f;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _sr = GetComponentInChildren<SpriteRenderer>();
        _playerAnimEvent = GetComponentInChildren<PlayerAnimEvent>();

        if (PlayerManager.Instance.player.facingDir == -1)
        {
            transform.Rotate(0, 180, 0);
        }
    }

    public void Attack()
    {
        Color newColor = _sr.color;
        newColor.a = 1f;
        _sr.color = newColor;
        _isFade = false;

        int randomInteger = UnityEngine.Random.Range(1, 4);
        _animator.SetInteger("AttackNumber", randomInteger);
        _animator.SetBool("Attack", true);
    }

    private void Update()
    {
        if (_playerAnimEvent.IsTriggered())
        {
            _animator.SetBool("Attack", false);
            _isFade = true;
        }

        if (_isFade)
        {
            Color currentColor = _sr.color;
            currentColor.a = Mathf.MoveTowards(currentColor.a, 0, _fadeSpeed * Time.deltaTime);
            _sr.color = currentColor;

            if (currentColor.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}