using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    public Enemy enemy;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        ChaseTarget(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        ChaseTarget(collision);
    }

    private void ChaseTarget(Collider2D collision)
    {
        bool findSuji = collision != null && collision.gameObject.CompareTag("Player");

        if (findSuji)
        {
            SujiController suji = collision.gameObject.GetComponent<SujiController>();
            bool isNotHide = suji != null && !suji.IsHiding;


            if (isNotHide)
            {
                // 수지 추격을 시작
                ChaseState state = ChaseState.GetInstance();
                enemy.StateMachine.SetState(enemy, state);
                state.SetTarget(collision.gameObject.transform);
            }
        }
    }
}