using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public static CameraManager Instance { get { return _instance; } }
    public Transform followingTarget;
    public float moveSpeed;
    public string VILLAGE { get; private set; } = "Village";
    public string MAINROON { get; private set; } = "Shaman_MainRoomDoor";
    public string WAREHOUSE { get; private set; } = "Shaman_WarehouseDoor";
    public string BATHROOM { get; private set; } = "Shaman_BathroomDoor";



    private static CameraManager _instance = null;
    private Vector2 canMoveAreaCenter;       // 카메라 이동 가능영역 중심
    private Vector2 canMoveAreaSize;         // 카메라 이동 가능영역 크기
    private CameraManager() { }
    private float cameraHalfWidth;          // 카메라의 월드 공간에서의 가로 길이
    private float cameraHalfHeight;         // 카메라의 월드 공간에서의 세로 길이

    public string tagKeyName
    {
        set
        {
            
            if (!_tagKeyName.Equals(value))
            {
                _tagKeyName = value;
                OnChangeCameraVector(_tagKeyName);
            }
        }
    }
    private string _tagKeyName = "";
    private Dictionary<string, (Vector2, Vector2)> dictionary = new Dictionary<string, (Vector2, Vector2)>();






    void Awake()
    {

        if(_instance == null)
        {
            _instance = this;
            cameraHalfHeight = Camera.main.orthographicSize;
            cameraHalfWidth = Screen.width * cameraHalfHeight / Screen.height;
            DontDestroyOnLoad(this.gameObject);
            return;
        }

        DestroyImmediate(this.gameObject);
    }


    void Start()
    {
        dictionary.Clear();
        dictionary.Add(VILLAGE, (new Vector2(-65f, 18.8f), new Vector2(400f, 48f)));
        dictionary.Add(MAINROON, (new Vector2(204.1f, -106.9f), new Vector2(123.5f, 49.6f)));
        dictionary.Add(WAREHOUSE, (new Vector2(342.4f, -284.8f), new Vector2(123.7f, 53.4f)));
        dictionary.Add(BATHROOM, (new Vector2(192.9f, -467.6f), new Vector2(101.2f, 48.9f)));
        OnChangeCameraVector(VILLAGE);
    }


    private void OnChangeCameraVector(string tagKeyName)
    {
        canMoveAreaCenter = dictionary[tagKeyName].Item1;
        canMoveAreaSize = dictionary[tagKeyName].Item2;
    }

    void LateUpdate()                   /* 카메라 이동은 LateUpdate()에서 처리 */
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        Vector3 targetPosition = new Vector3(followingTarget.position.x,
                                             this.transform.position.y,
                                             this.transform.position.z);

        this.transform.position = Vector3.Lerp(this.transform.position,
                                               targetPosition,
                                               moveSpeed * Time.deltaTime);

        float restrictionAreaX = canMoveAreaSize.x * 0.5f - cameraHalfWidth;
        float clampX = Mathf.Clamp(this.transform.position.x, -restrictionAreaX + canMoveAreaCenter.x, restrictionAreaX + canMoveAreaCenter.x);

        float restrictionAreaY = canMoveAreaSize.y * 0.5f - cameraHalfHeight;
        float clampY = Mathf.Clamp(this.transform.position.y, -restrictionAreaY + canMoveAreaCenter.y, restrictionAreaY + canMoveAreaCenter.y);

        this.transform.position = new Vector3(clampX, clampY, this.transform.position.z);
    }
}
