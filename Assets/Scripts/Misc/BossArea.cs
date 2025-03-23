using System;
using UnityEngine;

public class BossArea : MonoBehaviour
{
    public Transform teleportPosition1;
    public Transform teleportPosition2;
    public Collider2D cd;

    private void Awake()
    {
        cd = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManager.Instance.IsBossDead) return;

        if (other.gameObject.tag == "player")
        {
            GameManager.Instance.IsChallengeBoss = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (GameManager.Instance.IsBossDead) return;

        if (other.gameObject.tag == "player")
        {
            GameManager.Instance.IsChallengeBoss = false;
        }
    }
}