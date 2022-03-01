using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleGate : AbstractGate
{

    public GameObject doorAxis;
    public SujiController suji;
    public CameraManager _cameraManager;
    private Vector2 roomVector;

    protected override IEnumerator Open()
    {
        if (doorAxis != null)
        {
            if (_isOpening)
                yield break;


            _audioSource.Play();
            StartCoroutine(GatePassDelay());
            _isOpening = true;

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
            _isOpening = false;
        }
    }

    public override void OnEvent(string tag)
    {


        switch (tag)
        {
            case "SujiHouseDoor":
                DialogController.Instance.ShowDialog(tag);
                return;

            case "SujiHouseGate":
                if (!_isOpen)
                {
                    StartCoroutine(Open());
                }

                DialogController.Instance.ShowDialog(tag, _isOpen);
                return;


            case "Shaman_MainRoomDoor":
            case "Shaman_BathroomDoor":
            case "Village":
                if (!_isOpen)
                {
                    StartCoroutine(Open());
                    break;
                }

                roomVector = RoomVectorManager.Instance.GetRoomVector(tag);
                FadeInOutController.Instance.FadeIn(false);
                suji.MoveAnotherRoom(roomVector);
                _cameraManager.tagKeyName = tag;
                break;


            case "Shaman_WarehouseDoor":
                if (GameDataManager.Instance.IsKeyOn(tag))
                {
                    if (!_isOpen)
                    {
                        StartCoroutine(Open());
                        break;
                    }

                    roomVector = RoomVectorManager.Instance.GetRoomVector(tag);
                    FadeInOutController.Instance.FadeIn(false);
                    suji.MoveAnotherRoom(roomVector);
                    _cameraManager.tagKeyName = tag;
                }
                else
                {
                    DialogController.Instance.ShowDialog(tag + "NoKey");
                }
                break;

            //case "Shaman_OutsideDoor_Hidden":
            //    roomVector = RoomVectorManager.Instance.GetRoomVector(tag);
            //    FadeInOutController.Instance.FadeIn(false);
            //    suji.MoveAnotherRoom(roomVector);
            //    break;

            case "Shaman_From_Warehouse_To_MainRoom_Hidden":
                if (!_isOpen)
                {
                    StartCoroutine(Open());
                    break;
                }

                roomVector = RoomVectorManager.Instance.GetRoomVector(tag);
                FadeInOutController.Instance.FadeIn(false);
                suji.MoveAnotherRoom(roomVector);
                _cameraManager.tagKeyName = _cameraManager.MAINROON;
                break;

            case "Shaman_OutsideDoor_From_Bath_Hidden":
                if (!_isOpen)
                {
                    StartCoroutine(Open());
                    break;
                }

                roomVector = RoomVectorManager.Instance.GetRoomVector(tag);
                FadeInOutController.Instance.FadeIn(false);
                suji.MoveAnotherRoom(roomVector);
                _cameraManager.tagKeyName = _cameraManager.VILLAGE;
                break;
        }

        if(_isOpen)
            CloseDoor();
    }

    private void CloseDoor()
    {
        doorAxis.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x,
                                                     0f,
                                                     this.transform.eulerAngles.z);
        _isOpen = false;
    }
}