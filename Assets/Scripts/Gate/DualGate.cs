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
            if (_isOpening)
                yield break;

            _audioSource.Play();
            StartCoroutine(GatePassDelay());
            _isOpening = true;

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
            _isOpening = false;
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
                    {

                        _audioSource.clip = effClips.Find(x => x.name.Equals("Sound_Eff_Interaction"));
                        DialogController.Instance.ShowDialog(tag + "NoKey");
                        _audioSource.Play();
                    }
                    else
                    {
                        _audioSource.clip = effClips.Find(x => x.name.Equals("Sound_Eff_DualDoor"));
                        StartCoroutine(Open());
                    }
                }

                else if (!_isOpen && GameDataManager.Instance.hasShrineKey)
                {
                    StartCoroutine(Open());
                    LoadingSceneManager.LoadScene("Ending_Scene");
                }
                break;
        }
    }
}
