using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public Transform followingTarget;
    public float moveSpeed;
    public Vector2 canMoveAreaCenter;       // ī�޶� �̵� ���ɿ��� �߽�
    public Vector2 canMoveAreaSize;         // ī�޶� �̵� ���ɿ��� ũ��

    private float cameraHalfWidth;          // ī�޶��� ���� ���������� ���� ����
    private float cameraHalfHeight;         // ī�޶��� ���� ���������� ���� ����



    void Awake()
    {
        cameraHalfHeight = Camera.main.orthographicSize;
        cameraHalfWidth = Screen.width * cameraHalfHeight / Screen.height;

    }

    void LateUpdate()                   /* ī�޶� �̵��� LateUpdate()���� ó�� */
    {
        FollowTarget();
    }


    private void OnDrawGizmos()         /* ī�޶� Ÿ�ٿ� ���� �̵��� ������ ������ ǥ�� */
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