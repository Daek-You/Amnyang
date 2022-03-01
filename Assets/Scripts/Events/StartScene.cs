using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StartScene : MonoBehaviour
{

    public Image image;
    private float startTimer = 0f;

    void Start()
    {
        startTimer = Time.time;
    }


    void Update()
    {
        if(Time.time >= startTimer + 17f)
        {
            float alphaValue = Random.Range(0f, 0.15f);
            image.color = new Color(1f, 0f, 0f, alphaValue);
        }
    }
}
