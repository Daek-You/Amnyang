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
                    StartCoroutine(Open());
                    break;
                }

                DialogController.Instance.ShowDialog(tag, _isOpen);
                break;

            case "Shrine":
                if (!_isOpen && !GameDataManager.Instance.hasShrineKey)
                {
                if (!GameDataManager.Instance.IsKeyOn(tag)) 
                    DialogController.Instance.ShowDialog(tag + "NoKey");
                else
                    StartCoroutine(Open());
                }

                else if (!_isOpen && GameDataManager.Instance.hasShrineKey)
                {
                    StartCoroutine(Open());
                    /// 게임 엔딩
                    LoadingSceneManager.LoadScene("Ending_Scene");
                }
                break;
        }
    }
}
