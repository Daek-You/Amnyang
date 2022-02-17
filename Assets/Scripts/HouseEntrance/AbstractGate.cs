using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AbstractGate : MonoBehaviour
{

    public GameObject rightDoorAxis;
    public GameObject leftDoorAxis;

    protected float rotateSpeed = 2f;
    protected const float MIN_ANGLE = 0f;
    protected const float MAX_ANGLE = 90f;
    protected const float FLOAT_APPROXIMATLEY = 2f;


    public abstract void Open();
    public virtual void OnEvent() { }

    protected bool _Approximately(float x, float y)
    {
        float compareAbsValue = (x - y) > 0f ? x - y : y - x;

        if (compareAbsValue <= FLOAT_APPROXIMATLEY)
            return true;
        return false;
    }
}
