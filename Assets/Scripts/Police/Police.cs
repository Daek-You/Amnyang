using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Police : MonoBehaviour {

    public GameObject police;
    public Animator animator;

    private float rightMax = -10.0f;  //좌로 이동가능한 (x)최대값
    private float leftMax = -200.0f;  //우로 이동가능한 (x)최대값
    private float currentXPosition;   //현재 위치(x) 저장할 변수
    private float currentYPosition;   //현재 위치(y) 저장할 변수
    private float currentScale;      //현재 스케일(x) 저장할 변수
    public float direction = -1f;    //방향
    public float velocity = 10.0f;   //속도


    public bool isFind = false;
    public bool isWalk = true;

    // Start is called before the first frame update
    void Start()
    {
        currentXPosition = police.transform.position.x;
        currentScale = police.transform.localScale.x;
        currentYPosition = police.transform.position.y;
    }

    void Update()
    {
        MoveController();
    }

    void MoveController()
    {
        currentXPosition += Time.deltaTime * direction * velocity;

        if (currentXPosition >= rightMax)
        {
            currentXPosition = rightMax;
            LeftTurn();
        }

        else if (currentXPosition <= leftMax)
        {
            currentXPosition = leftMax;
            RightTurn();
        }

        police.transform.position = new Vector2(currentXPosition, currentYPosition);
    }
    
    void AnimatorController()   
    {
        if (isWalk == true)
        {
            animator.SetBool("isWalk", true);
            velocity = 5;
        }
        if (isWalk == false)
        {
            animator.SetBool("isWalk", false);
            velocity = 0;
        }
        if (isFind == true)
            animator.SetBool("isFInd", true);
        if (isFind == false) 
            animator.SetBool("isFInd", false);
    }
    public void LeftTurn()
    {
        direction = -1;
        police.transform.localScale = new Vector2(currentScale, currentScale);
    }

    public void RightTurn()
    {
        direction = 1;
        police.transform.localScale = new Vector2(currentScale * -1, currentScale);
    }


}