using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathArea : MonoBehaviour
{
    Collider2D collDeath;

    private void Start()
    {
        collDeath = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
            Debug.Log("게임 종료");
    }
}
