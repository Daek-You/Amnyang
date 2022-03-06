using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EndingEvent : MonoBehaviour
{

    public Text text;
    public float speed;

    void OnEnable()
    {
        StartCoroutine("Ending");
    }


    IEnumerator Ending()
    {
        while (true)
        {
            text.rectTransform.position = new Vector3(960f, text.rectTransform.position.y + speed, 0f);
            yield return null;
        }
    }
}
