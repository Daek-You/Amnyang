using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualGate : AbstractGate
{
    public GameObject rightDoorAxis;
    public GameObject leftDoorAxis;
 

    protected override IEnumerator Open()
    {
        if(rightDoorAxis != null && leftDoorAxis != null)
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

            _isOpen = true;
        }
    }


    public override void OnEvent(string tag)
    {

        switch (tag)
        {
            case "HouseEntrance":
                if (!_isOpen)
                {
                    /// "불쾌하게 생긴 대문이야" 텍스트 출력 요청 -> OnEvent에서 처리
                    StartCoroutine(Open());
                }

                else
                {
                    DialogCont.Instance.ShowDialog(tag, _isOpen);
                    /// "문은 이미 열려있어" 텍스트 출력 요청
                }
                break;

            case "Shrine":
                if(!_isOpen && !GameDataManager.Instance.hasShrineKey)
                {
                    /// "열쇠가 있어야 할 것 같아" 텍스트 출력 요청
                    DialogCont.Instance.ShowDialog(tag);
                }

                else if (!_isOpen && GameDataManager.Instance.hasShrineKey)
                {
                    StartCoroutine(Open());
                    /// 게임 엔딩
                }
                break;
        }
    }
}
