using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShamanFrontDoor : AbstractGate
{

    public Collider2D gateGoalKeeper;
    private bool isOpen = false;
    private WaitForSeconds gatePassWaitTime = new WaitForSeconds(0.5f);



    public override void Open()
    {
        StartCoroutine(GateOpen());
    }


    public override void OnEvent()
    {
    }


    IEnumerator GatePassDelay()
    {
        yield return gatePassWaitTime;

        if (!isOpen)
        {
            gateGoalKeeper.gameObject.SetActive(false);
        }
    }

    IEnumerator GateOpen()
    {
        StartCoroutine(GatePassDelay());

        while (true)
        {
            float rDoorCurEulerY = rightDoorAxis.transform.eulerAngles.y;
            float lDoorCurEulerY = leftDoorAxis.transform.eulerAngles.y;
            bool isCompleteOpen = _Approximately(rDoorCurEulerY, MAX_ANGLE) && _Approximately(lDoorCurEulerY, MAX_ANGLE);

            if (isCompleteOpen)
                break;

            rightDoorAxis.transform.eulerAngles = new Vector3(0f, Mathf.LerpAngle(rDoorCurEulerY, MAX_ANGLE, Time.deltaTime * rotateSpeed), 0f);
            leftDoorAxis.transform.eulerAngles = new Vector3(0f, Mathf.LerpAngle(lDoorCurEulerY, MAX_ANGLE, Time.deltaTime * rotateSpeed), 0f);
            yield return null;
        }

        isOpen = true;
    }
}
