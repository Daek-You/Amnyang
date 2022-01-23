using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{

    public GameObject rightDoorAxis;
    public GameObject leftDoorAxis;
    public GameObject amulet;
    public Collider2D gateGoalKeeper;
    public float rotationSpeed;
    public bool controlSignal;

    private bool isGateOpen = false;
    private const float maxRotationValue = 90f;
    private const float minRotationValue = 0f;
    private const float FLOAT_COMPARISON_VALUE = 2f;
    private WaitForSeconds gatePassWaitTime = new WaitForSeconds(0.5f);


    void Update()
    {
        if (!isGateOpen && controlSignal)
        {
            StartCoroutine(GateOpen());
        }

        else if (isGateOpen && controlSignal)
        {
            StartCoroutine(GateClose());
        }

    }

    IEnumerator GatePassDelay(bool isOpening)
    {
        yield return gatePassWaitTime;

        if (isOpening)
            gateGoalKeeper.gameObject.SetActive(false);
        else
            gateGoalKeeper.gameObject.SetActive(true);
    }

    IEnumerator GateClose()
    {
        controlSignal = false;
        amulet.SetActive(true);  // 부적은 어떻게 처리해야할 지 아직 안 정해서 활성화/비활성화 방식으로 일단 해둠
        StartCoroutine(GatePassDelay(false));

        while (true)
        {
            float rDoorCurEulerY = rightDoorAxis.transform.eulerAngles.y;
            float lDoorCurEulerY = leftDoorAxis.transform.eulerAngles.y;
            bool isCompleteClose = _Approximately(rDoorCurEulerY, minRotationValue) && _Approximately(lDoorCurEulerY, minRotationValue);

            if (isCompleteClose)
                break;

            rightDoorAxis.transform.eulerAngles = new Vector3(0f, Mathf.LerpAngle(rDoorCurEulerY, minRotationValue, Time.deltaTime * rotationSpeed), 0f);
            leftDoorAxis.transform.eulerAngles = new Vector3(0f, Mathf.LerpAngle(lDoorCurEulerY, minRotationValue, Time.deltaTime * rotationSpeed), 0f);
            yield return null;
        }

        isGateOpen = false;
    }

    IEnumerator GateOpen()
    {
        controlSignal = false;
        amulet.SetActive(false);  // 부적은 어떻게 처리해야할 지 아직 안 정해서 우선 문 열릴 때 잠시 비활성화해둠
        StartCoroutine(GatePassDelay(true));

        while (true)
        {
            float rDoorCurEulerY = rightDoorAxis.transform.eulerAngles.y;
            float lDoorCurEulerY = leftDoorAxis.transform.eulerAngles.y;
            bool isCompleteOpen = _Approximately(rDoorCurEulerY, maxRotationValue) && _Approximately(lDoorCurEulerY, maxRotationValue);

            if (isCompleteOpen)
                break;

            rightDoorAxis.transform.eulerAngles = new Vector3(0f, Mathf.LerpAngle(rDoorCurEulerY, maxRotationValue, Time.deltaTime * rotationSpeed), 0f);
            leftDoorAxis.transform.eulerAngles = new Vector3(0f, Mathf.LerpAngle(lDoorCurEulerY, maxRotationValue, Time.deltaTime * rotationSpeed), 0f);
            yield return null;
        }

        isGateOpen = true;
    }


    /// <summary>
    /// Mathf.Approximately를 쓰니, Mathf.LerpAngle 함수 특성상 목표값에 가까워질수록 증감폭이 작아지기 때문에
    /// 조건 충족까지 너무 상당한 시간이 걸림
    /// ex) 90도까지 돌린다면, 89.90030, 89.90031, ... 이런 식으로 너무 느리게 증가함
    /// 그래서 "threshold 값"(FLOAT_COMPARISON_VALUE)을 하나 지정하고, 따로 함수를 만들었음
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private bool _Approximately(float x, float y)
    {
        float absCalcValue = (x - y) > 0f ? x - y : y - x;
        if (absCalcValue <= FLOAT_COMPARISON_VALUE)
            return true;
        return false;
    }
}
