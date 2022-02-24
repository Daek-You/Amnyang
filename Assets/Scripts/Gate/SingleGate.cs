using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleGate : AbstractGate
{

    public GameObject doorAxis;


    protected override IEnumerator Open()
    {
        if(doorAxis != null)
        {
            StartCoroutine(GatePassDelay());


            while (true)
            {
                float currentDoorEulerY = doorAxis.transform.eulerAngles.y;
                bool isCompleteOpen = _Approximately(currentDoorEulerY, MAX_ANGLE);

                if (isCompleteOpen)
                    break;

                doorAxis.transform.eulerAngles = new Vector3(0f, Mathf.LerpAngle(currentDoorEulerY, MAX_ANGLE, Time.deltaTime * rotateSpeed), 0f);
                yield return null;
            }

            _isOpen = true;
        }
    }

    public override void OnEvent(string tag)
    {
        switch (tag)
        {
            case "SujiHouseDoor":
                /// "꼭 밝혀내야 해. 아직은 들어갈 때가 아니야" 텍스트 출력 요청
                DialogController.Instance.ShowDialog(tag);
                break;

            case "SujiHouseGate":
                if (!_isOpen)
                {
                    /// "이제 출발해보자." 텍스트 출력 요청
                    StartCoroutine(Open());
                    DialogController.Instance.ShowDialog(tag, _isOpen);
                }
                else
                {
                    DialogController.Instance.ShowDialog(tag, _isOpen);
                }
                break;

            case "Shaman_KitchenDoor":
                StartCoroutine(Open());


                break;

            case "Shaman_MainRoomDoor":
                StartCoroutine(Open());

                break;

            case "Shaman_BathroomDoor":
                StartCoroutine(Open());

                break;
        }
    }
}