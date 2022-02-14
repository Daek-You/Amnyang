using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SujiHouse : MonoBehaviour
{
    private Animator animator;
    private int eventNumber;
    private const float MIN_DELAYTIME = 5f;
    private const float MAX_DELAYTIME = 10f;


    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetInteger("EventNumber", 0);
        Invoke("OnEvent", Random.Range(MIN_DELAYTIME, MAX_DELAYTIME));
    }

    private void OnEvent()
    {
        eventNumber = Random.Range(1, 4);
        animator.SetInteger("EventNumber", eventNumber);

        float delayTime = Random.Range(MIN_DELAYTIME, MAX_DELAYTIME);
        Invoke("OnEvent", delayTime);
    }
}
