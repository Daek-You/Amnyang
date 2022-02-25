using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AbstractGate : MonoBehaviour
{
    public bool IsOpen { get { return _isOpen; } }

    protected float rotateSpeed = 2f;
    protected const float MIN_ANGLE = 0f;
    protected const float MAX_ANGLE = 90f;
    protected const float FLOAT_APPROXIMATLEY = 2f;
    protected bool _isOpen = false;


    //private Collider2D gateGoalKeeper;
    public Collider2D gateGoalKeeper;
    private WaitForSeconds gatePassWaitTime = new WaitForSeconds(0.5f);


    protected void Start()
    {
        //gateGoalKeeper = GetComponentInChildren<Collider2D>();
    }



    protected abstract IEnumerator Open();
    public abstract void OnEvent(string tag);




    protected bool _Approximately(float x, float y)
    {
        float compareAbsValue = (x - y) > 0f ? x - y : y - x;

        if (compareAbsValue <= FLOAT_APPROXIMATLEY)
            return true;
        return false;
    }

    protected IEnumerator GatePassDelay()
    {
        yield return gatePassWaitTime;

        if (!_isOpen && gateGoalKeeper != null)
        {
            gateGoalKeeper.gameObject.SetActive(false);
        }
    }
}
