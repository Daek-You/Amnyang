using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShamanDetect : MonoBehaviour
{

    public Shaman shaman;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Find(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Find(collision);
    }


    private void Find(Collider2D collision)
    {
        if (collision != null)
        {
            SujiController suji = collision.GetComponent<SujiController>();
            bool findSuji = collision.gameObject.CompareTag("Player") && !suji.IsHiding;

            if (findSuji && shaman.ShamanStateMachine.currentState != ChaseState.GetInstance())
            {
                shaman.ShamanStateMachine.SetState(shaman, ShamanFeelStrange.GetInstance());
            }
        }
    }
}
