using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void StateEnter(Enemy enemy);
    void StateFixedUpdate(Enemy enemy);
    void StateUpdate(Enemy enemy);
    void StateExit(Enemy enemy);
}


public class StateMachine
{
    public IState currentState { get; private set; }


    public StateMachine(Enemy enemy, IState defaultState)
    {
        currentState = defaultState;
        currentState.StateEnter(enemy);
    }
    public void SetState(Enemy enemy, IState state)
    {

        if (currentState == null || currentState == state)
        {
            Debug.Log("상태를 변경할 수 없습니다.");
            return;
        }

        currentState.StateExit(enemy);
        currentState = state;

        SoundPlayType soundType = (currentState == FeelStrange.GetInstance()) ? SoundPlayType.Fast : SoundPlayType.Slow;
        enemy._BGMManager.OnPlay(currentState, soundType);
        currentState.StateEnter(enemy);
    }

    public void UpdateState(Enemy enemy)
    {
        currentState.StateUpdate(enemy);
    }

    public void FixedUpdateState(Enemy enemy)
    {
        currentState.StateFixedUpdate(enemy);
    }
}

