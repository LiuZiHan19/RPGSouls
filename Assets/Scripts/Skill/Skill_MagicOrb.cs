using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_MagicOrb : Skill
{
    [SerializeField] private GameObject magicOrbPrefab;
    [SerializeField] private float orbitSpeed = 360f;
    [SerializeField] private float orbitRadius = 2f;
    [SerializeField] private float duration = 7.5f;
    [SerializeField] private float amount = 5f;

    public override void Release(params object[] parameters)
    {
        base.Release(parameters);
        for (int i = 0; i < amount; i++)
        {
            
        }
    }
}
