using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SujiMoveController : MonoBehaviour
{

    public float walkSpeed;
    public float jumpPower;
    public Transform sujiTransform;
    public GameObject suji;
    public InteractiveObject InteractiveObject { set { _interactiveObject = value; } }
    private InteractiveObject _interactiveObject;


    private Animator animator;
    private Rigidbody2D _rigidBody;
    private WaitForSeconds landingDelay = new WaitForSeconds(0.5f);
    private float walkDirection;
    private float initScaleX;
    private const float JUMP_CHARGING_DELAY = 0.55f;
    private bool hasControl;
    private bool isRunning;
    private bool isJumping;
    private bool isHiding;
    public bool IsHiding { get { return isHiding; }}




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

        if (Input.GetButtonDown("Interaction"))
        {
            Interaction();
        }
    }

    void FixedUpdate()
    {
        TurnOtherSide();
        Move();
    }




    private void Interaction()
    {
        if (_interactiveObject == null)
            return;

        _interactiveObject.Interaction();
    }

    public void Hide()
    {
        bool sujiActiveState = suji.activeSelf;

        if (sujiActiveState)
        {
            isHiding = true;
            suji.SetActive(false);
        }
        else
        {
            isHiding = false;
            suji.SetActive(true);
        }
    }

    private void Jump()
    {
        isJumping = true;

        if (!hasControl)
        {
            animator.SetBool("SargentJump", true);
            Invoke("_Jump", JUMP_CHARGING_DELAY);
        }
        else
        {
            animator.SetBool("MoveJump", true);
            _Jump();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Jumpable_Floor") && isJumping)
        {
            if (animator.GetBool("SargentJump"))
            {
                animator.SetTrigger("Landing");
                animator.SetBool("SargentJump", false);
                StartCoroutine(LandingDelay());
                return;
            }
            
            animator.SetBool("MoveJump", false);
            isJumping = false;
        }
    }

    private void _Jump()
    {
        _rigidBody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

    IEnumerator LandingDelay()
    {
        yield return landingDelay;
        isJumping = false;
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
        if (!hasControl || isJumping)
            return;

        var scaleX = sujiTransform.localScale.x;
        if (Mathf.Approximately(walkDirection * scaleX, initScaleX))
            return;

        var scaleY = sujiTransform.localScale.y;
        var scaleZ = sujiTransform.localScale.z;

        sujiTransform.localScale = new Vector3(-scaleX, scaleY, scaleZ);
    }
}
