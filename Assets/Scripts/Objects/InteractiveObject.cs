using System.Collections;
using System.Collections.Generic;
using ObjectState;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{

    [SerializeField] private SpriteRenderer hidingSprite;
    [SerializeField] private bool isDynamicHiddenSpace;        // 상호작용 요청한 시점의 플레이어 위치에 숨을 경우
    [SerializeField] private ObjectType objectType;
    private SujiController suji;
    private string objectTag;

    public GameObject Key;

    void Start()
    {
        objectTag = this.gameObject.tag;
    }


    public void Interaction()
    {

        switch ((int)objectType)
        {
            case (int)ObjectType.eSimpleEventObject:
                DialogController.Instance.ShowDialog(objectTag);

                break;

            case (int)ObjectType.eCanHideObject:
                if(hidingSprite != null)
                    suji.Hide(hidingSprite, isDynamicHiddenSpace);

                break;

            case (int)ObjectType.eItemObject:
                /// GameDataManager 오브젝트를 만들어서, 이 오브젝트의 아이템 데이터를 전달하여 저장
                /// 역시, 싱글톤 패턴으로 구현하면 좋을 듯
                /// ex) 열쇠를 먹기 전 : key = 0     /  열쇠 먹은 후 : key = 1
                if (GameDataManager.Instance.IsKeyOn(tag) == false)     /// 아직 키를 갖고있지 않으면
                {
                    GameDataManager.Instance.GetKey(tag);               /// 키 = 1;
                    DialogController.Instance.ShowDialog(tag);          /// 키를 얻었다.
                    Key.SetActive(true);
                }
                else
                {
                    DialogController.Instance.ShowDialog(tag + "already");/// 이미 키를 갖고있어.
                }
                break;

            case (int)ObjectType.eDoorObject:
                AbstractGate gate = GetComponentInChildren<AbstractGate>();

                if (gate != null)
                    gate.OnEvent(objectTag);
                break;
        }
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            suji = collision.gameObject.GetComponent<SujiController>();

            if (suji != null)
            {
                suji.InteractiveObject = this;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            suji.InteractiveObject = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (suji != null)
            {
                suji.InteractiveObject = null;
            }
        }
    }
}
