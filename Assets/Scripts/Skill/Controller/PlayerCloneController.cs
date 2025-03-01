using System;
using UnityEngine;

public class PlayerCloneController : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void Attack()
    {
        int randomInteger = UnityEngine.Random.Range(1, 4);
        switch (randomInteger)
        {
            case 1:
                _animator.SetInteger("AttackNumber", 1);
                break;
            case 2:
                _animator.SetInteger("AttackNumber", 2);
                break;
            case 3:
                _animator.SetInteger("AttackNumber", 3);
                break;
        }
    }
}