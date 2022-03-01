using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPlayer : MonoBehaviour
{

    public Enemy enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SujiController suji = collision.GetComponent<SujiController>();
            suji.OnDie();
            enemy.gameObject.SetActive(false);
        }
    }
}
