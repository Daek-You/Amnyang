using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPlayer : MonoBehaviour
{

    public Enemy enemy;
    private SujiController suji;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Die(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Die(collision);
    }

    private void Die(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SujiController suji = collision.GetComponent<SujiController>();

            if (suji != null && !suji.IsHiding)
            {
                suji.OnDie();
                enemy.gameObject.SetActive(false);
            }
        }
    }

}
