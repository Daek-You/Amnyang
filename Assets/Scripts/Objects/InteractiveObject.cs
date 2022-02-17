using System.Collections;
using System.Collections.Generic;
using ObjectState;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{

    public ObjectType objectType;
    private SujiController suji;
    private string objectTag;


    void Start()
    {
        objectTag = this.gameObject.tag;
    }


    public void Interaction()
    {
        if(objectType == ObjectType.eTextPrintObject)
        {
            /// TextManager에게 이 상호작용 오브젝트의 "objectTag"를 매개변수로 해서 전달
            /// ex) TextManager.ShowDialog(string objectTag);
            /// TextManager에서는 이 태그를 key로 하여, 거기에 대응되는 텍스트(value)를 가져오면 됨.
            /// 싱글톤(Singleton) 패턴으로 구현하면 좋을 듯하다.
        }

        if (objectType == ObjectType.eCanHideObject)
        {
            suji.Hide();
        }

        if(objectType == ObjectType.eItemObject)
        {
            /// GameDataManager 오브젝트를 만들어서, 이 오브젝트의 아이템 데이터를 전달하여 저장
            /// 역시, 싱글톤 패턴으로 구현하면 좋을 듯
            /// ex) 열쇠를 먹기 전 : key = 0     /  열쇠 먹은 후 : key = 1
        }

        if(objectType == ObjectType.eDoorObject)
        {
            AbstractGate gate = GetComponent<AbstractGate>();

            if (gate != null)
            {
                gate.Open();
            }
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (suji != null)
            {
                suji.InteractiveObject = null;
                suji = null;
            }
        }
    }
}
