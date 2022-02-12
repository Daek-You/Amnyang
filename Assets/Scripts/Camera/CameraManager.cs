using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public Transform followingTarget;
    public float moveSpeed;
    public Vector2 canMoveAreaCenter;       // 카메라 이동 가능영역 중심
    public Vector2 canMoveAreaSize;         // 카메라 이동 가능영역 크기

    private float cameraHalfWidth;          // 카메라의 월드 공간에서의 가로 길이
    private float cameraHalfHeight;         // 카메라의 월드 공간에서의 세로 길이



    void Awake()
    {
        cameraHalfHeight = Camera.main.orthographicSize;
        cameraHalfWidth = Screen.width * cameraHalfHeight / Screen.height;

    }

    void LateUpdate()                   /* 카메라 이동은 LateUpdate()에서 처리 */
    {
        FollowTarget();
    }


    private void OnDrawGizmos()         /* 카메라가 타겟에 따라 이동이 가능한 영역을 표시 */
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(canMoveAreaCenter, canMoveAreaSize);
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
