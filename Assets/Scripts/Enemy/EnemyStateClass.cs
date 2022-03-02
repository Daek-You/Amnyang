using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Idle : IState
{
    private static Idle Instance = new Idle();
    private Idle() { }
    public static Idle GetInstance() {  return Instance; }
    public const string BGM = "Sound_Bgm_Village";



    public void StateEnter(Enemy enemy)
    {
        if (enemy != null)
        {
            enemy.ThinkNextMovingRepeat();
            enemy._BGMManager.OnPlay(BGM, SoundPlayType.Slow);
        }
    }

    public void StateUpdate(Enemy enemy) { }

    public void StateFixedUpdate(Enemy enemy)
    {
        if (enemy != null)
        {
            enemy.Move();
        }
    }

    public void StateExit(Enemy enemy)
    {
        enemy.CancleThinking();
    }
}


public class FeelStrange : IState
{
    private static FeelStrange Instance = new FeelStrange();
    private const float DETECT_TIME = 5f;
    private float detectStartTime;
    public const string BGM = "Sound_Bgm_EnemyFeel";

    private FeelStrange() { }

    public static FeelStrange GetInstance()
    {
        return Instance;
    }

    public void StateEnter(Enemy enemy)
    {
        if (enemy != null)
        {
            enemy.Animator.SetBool("isFeel", true);
            enemy._BGMManager.OnPlay(BGM, SoundPlayType.Fast);
            detectStartTime = Time.time;
            enemy.FeelStrangeAround();
        }
    }

    public void StateFixedUpdate(Enemy enemy) { }

    public void StateUpdate(Enemy enemy)
    {
        if(enemy != null)
        {
            // 추격 중엔 감지할 필요가 없으므로
            if (enemy.StateMachine.currentState != ChaseState.GetInstance())
            {
                bool notFindHer = Time.time >= DETECT_TIME + detectStartTime;

                if (notFindHer)    // 'DETECT_TIME'만큼 주변을 둘러봤는데 없으면 다시 Idle 상태로
                {
                    enemy.StateMachine.SetState(enemy, Idle.GetInstance());
                }  
            }
        }
    }

    public void StateExit(Enemy enemy)
    {
        enemy.Animator.SetBool("isFeel", false);
    }
}


public class ChaseState : IState
{
    private static ChaseState Instance = new ChaseState();
    private Transform target;
    private float findStartTime;
    private float findingTime;
    private SujiController suji;
    private bool isStartTimer = false;
    private bool isCheckAround = false;
    public const string BGM = "Sound_Bgm_Chase";

    private ChaseState() { }
    

    public static ChaseState GetInstance()
    {
        return Instance;
    }

    public void StateEnter(Enemy enemy)
    {
        enemy.suji = suji;
        enemy.StartCoroutine("Wait", false);
        findingTime = Random.Range(10, 16);
        enemy._BGMManager.OnPlay(BGM, SoundPlayType.Fast);
    }



    public void StateFixedUpdate(Enemy enemy)
    {
        if(enemy != null)
        {
            if (enemy.IsCanChase && suji.IsHiding && !isCheckAround)
            {
                enemy.ThinkNextMovingRepeat();
                isCheckAround = true;
            }
            enemy.Chase(target);
        }
    }

    public void StateUpdate(Enemy enemy)
    {
        if (suji.IsHiding && enemy.IsCanChase)
        {
            if (!isStartTimer)
            {
                findStartTime = Time.time;
                isStartTimer = true;
            }

            CheckTimer(enemy);
        }


        if (isStartTimer)
        {
            CheckTimer(enemy);
        }

    }

    private void CheckTimer(Enemy enemy)
    {
        float currentTime = Time.time;
        if (currentTime >= findStartTime + findingTime)
        {
            enemy.StateMachine.SetState(enemy, Idle.GetInstance());
        }
    }


    public void StateExit(Enemy enemy)
    {
        enemy.ChangeMode(false);
        enemy.CancleThinking();
        enemy.IsCanChase = false;
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



