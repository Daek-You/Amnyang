using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SujiHouse : MonoBehaviour
{
    private Animator animator;
    private int eventNumber;
    private const float MIN_DELAYTIME = 5f;
    private const float MAX_DELAYTIME = 10f;
    private AudioSource _audioSource;

    void Start()
    {
        animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        animator.SetInteger("EventNumber", 0);
        Invoke("OnEvent", Random.Range(MIN_DELAYTIME, MAX_DELAYTIME));
    }

    private void OnEvent()
    {
        eventNumber = Random.Range(1, 4);

        if(eventNumber == 3)
        {
            _audioSource.Play();
        }

        animator.SetInteger("EventNumber", eventNumber);
        float delayTime = Random.Range(MIN_DELAYTIME, MAX_DELAYTIME);
        Invoke("OnEvent", delayTime);
    }
}
