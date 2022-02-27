using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class Shaman : MonoBehaviour
{
    public float moveSpeed;
    public ShamanStateMachine ShamanStateMachine { get { return _ShamanStateMachine; } }
    public Animator Animator { get { return _animator; } }
    public Light2D _haloLight;
    public SujiController suji { get; set; }
    public bool IsCanChase { get; set; } = false;


    private ShamanStateMachine _ShamanStateMachine;
    private int moveDirection;
    private Collider2D _collider;
    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private WaitForSeconds waitTimeBeforeTurn = new WaitForSeconds(1.5f);
    private WaitForSeconds waitTimeBeforeChase = new WaitForSeconds(1f);
    private float initScaleX;
    private SpriteRenderer[] _spriteRenderes;
    private const string CHASEMODE_LAYER = "ChaseMode_Shaman";
    private const string NORMALMODE_LAYER = "Normal_Shaman";
    private Color normalShamanColor;



    void Start()
    {
        _collider = GetComponent<Collider2D>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _ShamanStateMachine = new ShamanStateMachine(this, ShamanIdle.GetInstance());
        initScaleX = -this.transform.localScale.x;        // 얘는 왼쪽이 기본값이네
        _spriteRenderes = GetComponentsInChildren<SpriteRenderer>();
        normalShamanColor = _spriteRenderes[0].color;            // 어차피 모두 같은 컬러 사용
    }


    void Update()
    {
        if(_ShamanStateMachine != null)
        {
            _ShamanStateMachine.UpdateState(this);
        }
    }

    void FixedUpdate()
    {
        if (_ShamanStateMachine != null)
        {
            _ShamanStateMachine.FixedUpdateState(this);
        }
    }


    public void ThinkNextMovingRepeat()
    {
        moveDirection = Random.Range(-1, 2);
        float thinkDelayTime = Random.Range(1f, 3f);
        Invoke("ThinkNextMovingRepeat", thinkDelayTime);
    }

    public void CancleThinking()
    {
        _animator.SetBool("isWalk", false);
        moveDirection = 0;
        CancelInvoke("ThinkNextMovingRepeat");
    }

    public void Move()
    {
        TurnAround(false);
        bool isWalk = (moveDirection != 0) ? true : false;
        _animator.SetFloat("WalkMultiplier", 1f);
        _animator.SetBool("isWalk", isWalk);
        _rigidBody.velocity = new Vector2(moveDirection * moveSpeed, _rigidBody.velocity.y);
    }

    public void FeelStrangeAround()
    {
        StartCoroutine("Wait", true);
    }

    public void Chase(Transform target)
    {
        if (target != null && IsCanChase)
        {

            int targetDirection = CompareToTargetDirection(this.transform.position.x, target.position.x);
            if (!suji.IsHiding)
            {
                LookAt(targetDirection, true);
                moveDirection = targetDirection;
            }

            TurnAround(false);
            _animator.SetBool("isWalk", moveDirection != 0);
            _animator.SetFloat("WalkMultiplier", 2f);
            _rigidBody.velocity = new Vector2(moveDirection * moveSpeed * 3.5f, _rigidBody.velocity.y);
        }
    }


    private void LookAt (int targetDirection, bool defaultLeftLook)
    {

        if (defaultLeftLook)  // 왼쪽을 보고 있는 것이 기본값
        {
            switch (targetDirection)
            {
                case -1:
                case 1:

                    float currentLookAtDir = this.transform.localScale.x;

                    if(currentLookAtDir * targetDirection > 0f)
                    {
                        var scaleX = this.transform.localScale.x;
                        var scaleY = this.transform.localScale.y;
                        var scaleZ = this.transform.localScale.z;

                        this.transform.localScale = new Vector3(-scaleX, scaleY, scaleZ);
                    }
                    break;
            }
        }
    }


    private int CompareToTargetDirection(float myPositionX, float targetPositionX)
    {
        if (targetPositionX < myPositionX)
            return -1;
        else if (targetPositionX > myPositionX)
            return 1;

        return 0;
    }

    public void ChangeMode(bool isChaseMode)
    {
        if (isChaseMode)
        {
            _haloLight.gameObject.SetActive(true);
            foreach (SpriteRenderer renderer in _spriteRenderes)
            {
                renderer.sortingLayerName = CHASEMODE_LAYER;
                renderer.color = Color.black;
            }
        }

        else
        {
            _haloLight.gameObject.SetActive(false);
            foreach (SpriteRenderer renderer in _spriteRenderes)
            {
                renderer.sortingLayerName = NORMALMODE_LAYER;
                renderer.color = normalShamanColor;
            }
        }
    }

    IEnumerator Wait(bool isFeelStrangeMode)
    {
        yield return isFeelStrangeMode ? waitTimeBeforeTurn : waitTimeBeforeChase;

        if (isFeelStrangeMode)
        {
            TurnAround(isFeelStrangeMode);
            yield break;
        }

        ChangeMode(true);
        IsCanChase = true;
    }


    private void TurnAround(bool isFeelPlayerAround)
    {
        var scaleX = this.transform.localScale.x;
        var scaleY = this.transform.localScale.y;
        var scaleZ = this.transform.localScale.z;

        if (!isFeelPlayerAround)
        {
            if (moveDirection == 0)
                return;

            if (scaleX * moveDirection == initScaleX)
                return;
        }

        this.transform.localScale = new Vector3(-scaleX, scaleY, scaleZ);
    }

    public void MoveAnotherRoom(Vector2 roomVector)
    {
        transform.position = roomVector;
    }
}
