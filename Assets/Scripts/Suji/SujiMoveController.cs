using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SujiMoveController : MonoBehaviour
{

    public float walkSpeed;
    public float jumpPower;
    public Transform sujiTransform;

    private Animator animator;
    private Rigidbody2D _rigidBody;
    private float walkDirection;
    private float initScaleX;
    private const float JUMP_CHARGING_DELAY = 0.55f;      // "서전트 점프" 시 사용
    private bool hasControl;
    private bool isRunning;
    private bool isJumping;


    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();          
        initScaleX = sujiTransform.localScale.x;
    }

    void Update()
    {
        walkDirection = Input.GetAxisRaw("Horizontal");
        hasControl = !Mathf.Approximately(walkDirection, 0f);
        isRunning = Input.GetButton("Run");

        if(Input.GetButtonDown("Jump") && !isJumping)
        {
            Jump();
        }
    }
    void FixedUpdate()
    {
        TurnOtherSide();
        Move();
    }


    private void Jump() 
    {
        if (!hasControl && !isJumping)  // 제자리 점프일 경우
        {
            isJumping = true;
            animator.SetBool("Jump", true);
            Invoke("_Jump", JUMP_CHARGING_DELAY);     
        }
        /// 이동 중 점프 구현하기

    }

    private void _Jump()
    {
        _rigidBody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Jumpable_Floor"))
        {
            if (isJumping)
            {
                animator.SetTrigger("Landing");
                animator.SetBool("Jump", false);
                isJumping = false;
            }
        }
    }
    private void Move()
    {
        /* 0f : Idle, 0.5f : Walk, 1f : Run  */

        if (isJumping)
            return;

        if (hasControl && isRunning)
        {
            animator.SetFloat("Move", 1f);
            _rigidBody.velocity = new Vector2(walkDirection * walkSpeed * 2f, _rigidBody.velocity.y);
            return;
        }

        float walkValue = (hasControl && !isRunning) ? 0.5f : 0f;
        animator.SetFloat("Move", walkValue);
        _rigidBody.velocity = new Vector2(walkDirection * walkSpeed, _rigidBody.velocity.y);
    }
    private void TurnOtherSide()
    {
        if (!hasControl)
            return;

        if (isJumping)
            return;

        var scaleX = sujiTransform.localScale.x;
        if (Mathf.Approximately(walkDirection * scaleX, initScaleX))
            return;

        var scaleY = sujiTransform.localScale.y;
        var scaleZ = sujiTransform.localScale.z;

        sujiTransform.localScale = new Vector3(-scaleX, scaleY, scaleZ);
    }
}
