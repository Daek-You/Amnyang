using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ShaIState
{
    void StateEnter(Shaman shaman);
    void StateFixedUpdate(Shaman shaman);
    void StateUpdate(Shaman shaman);
    void StateExit(Shaman shaman);
}


public class ShamanStateMachine
{
    public ShaIState currentState { get; private set; }


    public ShamanStateMachine(Shaman shaman, ShaIState defaultState)
    {
        currentState = defaultState;
        currentState.StateEnter(shaman);
    }
    public void SetState(Shaman shaman, ShaIState state)
    {

        if (currentState == null || currentState == state)
        {
            Debug.Log("상태를 변경할 수 없습니다.");
            return;
        }

        currentState.StateExit(shaman);
        currentState = state;
        currentState.StateEnter(shaman);
    }

    public void UpdateState(Shaman shaman)
    {
        currentState.StateUpdate(shaman);
    }

    public void FixedUpdateState(Shaman shaman)
    {
        currentState.StateFixedUpdate(shaman);
    }
}

