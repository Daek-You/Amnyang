using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBlink : MonoBehaviour
{

    [SerializeField]
    private Animator animator;
    void Start()
    {
        Blink();
    }
    void Blink()
    {
        int time = Random.Range(2, 5);
        _Invoke(time);
    }

    void _Invoke(int time)
    {
        animator.SetTrigger("EyeBlink");
        Invoke("Blink", time);
    }
    
}
