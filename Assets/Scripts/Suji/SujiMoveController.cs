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
    private bool isRunning;


    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();          
        initScaleX = sujiTransform.localScale.x;
    }

    void Update()
    {
        walkDirection = Input.GetAxisRaw("Horizontal");
        isRunning = Input.GetButton("Run");
    }

    void FixedUpdate()
    {
        bool hasControl = !Mathf.Approximately(walkDirection, 0f);
        TurnOtherSide(hasControl);
        Move(hasControl);
    }

    private void Move(bool hasControl)   // 0f : Idle, 0.5f : Walk, 1f : Run
    {
        if (hasControl && isRunning)
        {
            animator.SetFloat("Move", 1f);
            _rigidBody.velocity = new Vector2(walkDirection * walkSpeed * 2f, _rigidBody.velocity.y);  // 일단 달리기는 걷는 속도의 2배로 해놓음
            return;
        }

        float walkValue = (hasControl && !isRunning) ? 0.5f : 0f;
        animator.SetFloat("Move", walkValue);
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
