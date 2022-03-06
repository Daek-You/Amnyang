using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodFlow : MonoBehaviour
{

    public GameObject suji;
    private Vector2 eventVectorCenter = new Vector2(210f, -119f);
    private Vector2 eventVectorSize = new Vector2(40f, 20f);
    private Animator _animator;
    private bool isOccurEvent = false;
    private AudioSource _audioSource;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(eventVectorCenter, eventVectorSize);
    }

    void Update()
    {
        bool canOccurEvent = Vector2.Distance(eventVectorCenter, suji.transform.position) <= 20f && (!isOccurEvent);
        if (canOccurEvent)
        {
            _animator.SetTrigger("Flow");
            _audioSource.Play();
            isOccurEvent = true;
        }
    }
}
