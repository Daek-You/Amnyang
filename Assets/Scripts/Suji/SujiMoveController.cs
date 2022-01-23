using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SujiMoveController : MonoBehaviour
{

    public float walkSpeed;
    public Transform sujiTransform;

    private Animator animator;
    private Rigidbody2D _rigidBody;
    private float walkDirection;
    private float initScaleX;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();          
        initScaleX = sujiTransform.localScale.x;
    }

    void Update()
    {
        walkDirection = Input.GetAxisRaw("Horizontal");
    }


    void FixedUpdate()
    {
        Walk();
    }

    private void Walk()
    {
        bool hasControl = !Mathf.Approximately(walkDirection, 0f);
        TurnOtherSide(hasControl);
        animator.SetBool("Walk", hasControl);
        _rigidBody.velocity = new Vector2(walkDirection * walkSpeed, _rigidBody.velocity.y);
    }


    private void TurnOtherSide(bool hasControl)
    {
        if (!hasControl)
            return;

        var scaleX = sujiTransform.localScale.x;
        if (Mathf.Approximately(walkDirection * scaleX, initScaleX))
            return;

        var scaleY = sujiTransform.localScale.y;
        var scaleZ = sujiTransform.localScale.z;

        sujiTransform.localScale = new Vector3(-scaleX, scaleY, scaleZ);
    }
}
