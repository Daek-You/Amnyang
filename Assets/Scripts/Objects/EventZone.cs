using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventZone : MonoBehaviour
{

    public bool isOneTimeEvent;
    private bool checkSwitch = false;
    private string objectTag;

    void Start()
    {
        objectTag = this.gameObject.tag;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isOneTimeEvent && !checkSwitch)  // 일회성 이벤트일 경우
            {
                ///Text.Mananger에게 텍스트 출력 요청
                DialogCont.Instance.ShowDialog(tag);
                checkSwitch = true;
            }

            else if (!isOneTimeEvent)            // 다회성 이벤트
            {
                
            }

        }
    }
}
