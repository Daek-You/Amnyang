using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTeleport : MonoBehaviour
{

    private Enemy enemy;


    void Start()
    {
        enemy = GetComponent<Enemy>();    
    }

    // Enemy의 컬리젼 레이어가 Door이면 그 문의 태그 이름을 가져옴
    // 룸벡터를 통해 다음 이동할 곳으로
    // 만약 플레이어가 


  
}
