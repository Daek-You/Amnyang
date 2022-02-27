using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShamanIdle : ShaIState
{
    private static ShamanIdle Instance = new ShamanIdle();
    private ShamanIdle() { }
    public static ShamanIdle GetInstance() {  return Instance; }

    public void StateEnter(Shaman shaman)
    {
        if (shaman != null)
        {
            shaman.ThinkNextMovingRepeat();
        }
    }

    public void StateUpdate(Shaman shaman) { }

    public void StateFixedUpdate(Shaman shaman)
    {
        if (shaman != null)
        {
            shaman.Move();
        }
    }

    public void StateExit(Shaman shaman)
    {
        shaman.CancleThinking();
    }
}


public class ShamanFeelStrange : ShaIState
{
    private static ShamanFeelStrange Instance = new ShamanFeelStrange();
    private const float DETECT_TIME = 5f;
    private float detectStartTime;


    private ShamanFeelStrange() { }

    public static ShamanFeelStrange GetInstance()
    {
        return Instance;
    }

    public void StateEnter(Shaman shaman)
    {
        if (shaman != null)
        {
            shaman.Animator.SetBool("isFeel", true);
            detectStartTime = Time.time;
            shaman.FeelStrangeAround();
        }
    }

    public void StateFixedUpdate(Shaman shaman) { }

    public void StateUpdate(Shaman shaman)
    {
        if(shaman != null)
        {
            // 추격 중엔 감지할 필요가 없으므로
            if (shaman.ShamanStateMachine.currentState != ChaseState.GetInstance())
            {
                bool notFindHer = Time.time >= DETECT_TIME + detectStartTime;

                if (notFindHer)    // 'DETECT_TIME'만큼 주변을 둘러봤는데 없으면 다시 ShamanIdle 상태로
                {
                    shaman.ShamanStateMachine.SetState(shaman, ShamanIdle.GetInstance());
                }  
            }
        }
    }

    public void StateExit(Shaman shaman)
    {
        shaman.Animator.SetBool("isFeel", false);
    }
}


public class ShamanChaseState : ShaIState
{
    private static ShamanChaseState Instance = new ShamanChaseState();
    private Transform target;
    private float findStartTime;
    private float findingTime;
    private SujiController suji;
    private bool isStartTimer = false;
    private bool isCheckAround = false;

    private ShamanChaseState() { }
    

    public static ShamanChaseState GetInstance()
    {
        return Instance;
    }

    public void StateEnter(Shaman shaman)
    {
        shaman.suji = suji;
        shaman.StartCoroutine("Wait", false);
        findingTime = Random.Range(10, 16);
    }



    public void StateFixedUpdate(Shaman shaman)
    {
        if(shaman != null)
        {
            if (shaman.IsCanChase && suji.IsHiding && !isCheckAround)
            {
                shaman.ThinkNextMovingRepeat();
                isCheckAround = true;
            }
            shaman.Chase(target);
        }
    }

    public void StateUpdate(Shaman shaman)
    {
        if (suji.IsHiding && shaman.IsCanChase)
        {
            if (!isStartTimer)
            {
                findStartTime = Time.time;
                isStartTimer = true;
            }

            float currentTime = Time.time;
            if (currentTime >= findStartTime + findingTime)
            {
                shaman.ShamanStateMachine.SetState(shaman, ShamanIdle.GetInstance());
            }
        }

        if (GameDataManager.Instance.isAreTheyInTheSamePlace == false)
        {
            ShamanTeleport.Instance.InvokeShamanTeleportToSuji();
        }

    }

    public void invokeTeleport()
    {
        ShamanTeleport.Instance.ShamanTeleportToSuji();
    }

    public void StateExit(Shaman shaman)
    {
        shaman.ChangeMode(false);
        shaman.CancleThinking();
        shaman.IsCanChase = false;
        isCheckAround = false;
        isStartTimer = false;
        target = null;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
        suji = this.target.gameObject.GetComponent<SujiController>();
    }

}



