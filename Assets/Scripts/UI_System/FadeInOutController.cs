using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FadeInOutController : MonoBehaviour
{

    public Image blackIMG;
    public static bool IsFinished = false;
    public static FadeInOutController Instance { get { return _instance; } }

    private static FadeInOutController _instance = null;
    private FadeInOutController() { }
    private const float SLOW_FADE_VALUE = 0.001f;
    private const float FADE_VALUE = 0.01f;
    
    private Canvas canvas;



    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }

        DestroyImmediate(this.gameObject);
    }


    void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
    }

    public void FadeIn(bool isSlow)
    {
        IsFinished = false;
        StartCoroutine("FadeInCoroutine", isSlow);
    }

    public void FadeOut(bool isSlow)
    {
        IsFinished = false;
        StartCoroutine("FadeOutCoroutine", isSlow);
    }


    private IEnumerator FadeInCoroutine(bool isSlow)
    {
        float fadeValue = 1f;

        while(fadeValue > 0f)
        {

            float value = isSlow ? SLOW_FADE_VALUE : FADE_VALUE;
            fadeValue -= value;
            yield return null;
            blackIMG.color = new Color(0f, 0f, 0f, fadeValue);
        }

        canvas.gameObject.SetActive(false);
        IsFinished = true;
    }

    private IEnumerator FadeOutCoroutine(bool isSlow)
    {
        float fadeValue = 0f;

        while (fadeValue < 1f)
        {
            float value = isSlow ? SLOW_FADE_VALUE : FADE_VALUE;

            fadeValue += value;
            yield return null;
            blackIMG.color = new Color(0f, 0f, 0f, fadeValue);
        }

        canvas.gameObject.SetActive(true);
        IsFinished = true;
    }
}
