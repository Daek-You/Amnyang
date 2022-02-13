using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSettings : MonoBehaviour
{

    public Transform followingTarget;
    public float followingSpeed;
    private float cameraPositionY;


    void Awake()
    {
        cameraPositionY = this.transform.position.y;
    }


    void LateUpdate()
    {
        FollowTarget();
    }


    private void FollowTarget()
    {
        // 카메라의 Z 위치값을 고정 (씬의 오브젝트보다 더 앞 쪽에 있어야 렌더링이 보인다.)
        Vector3 targetPosition = new Vector3(followingTarget.position.x,
                                           cameraPositionY,
                                           -10f);
        transform.position = Vector3.Lerp(transform.position,
                                          targetPosition,
                                          followingSpeed * Time.deltaTime);
    }
}
