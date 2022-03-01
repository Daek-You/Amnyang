using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FadeInOutController : MonoBehaviour
{

    public Image blackIMG;
    public Text gameOverTxt;
    public static bool IsFinished = false;
    public static FadeInOutController Instance { get { return _instance; } }

    private static FadeInOutController _instance = null;
    private FadeInOutController() { }
    private const float SLOW_FADE_VALUE = 0.001f;
    private const float FADE_VALUE = 0.008f;
    private WaitForSeconds waitTime = new WaitForSeconds(2f);
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
        StartCoroutine(FadeInCoroutine(false));
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

    public void ImmediateFadeOut()
    {
        IsFinished = false;
        blackIMG.color = new Color(0f, 0f, 0f, 1f);
        IsFinished = true;
    }

    public void TextFadeInOut()
    {
        gameOverTxt.gameObject.SetActive(true);
        IsFinished = false;
        StartCoroutine("TextFadeInOutCoroutine");
    }


    private IEnumerator TextFadeInOutCoroutine()
    {
        yield return waitTime;
        float fadeValue = 0f;

        while (fadeValue < 1f)
        {
            fadeValue += 0.01f;
            yield return null;
            gameOverTxt.color = new Color(1f, 0f, 0f, fadeValue);
        }

        yield return waitTime;

        while (fadeValue > 0f)
        {
            fadeValue -= 0.01f;
            yield return null;
            gameOverTxt.color = new Color(1f, 0f, 0f, fadeValue);
        }


        yield return waitTime;
        IsFinished = true;
        gameOverTxt.gameObject.SetActive(false);
        LoadingSceneManager.LoadScene("Village_Scene");
        FadeIn(true);
    }

    private IEnumerator FadeInCoroutine(bool isSlow)
    {
        float fadeValue = 1f;

        while (fadeValue > 0f)
        {

            float value = isSlow ? SLOW_FADE_VALUE : FADE_VALUE;
            fadeValue -= value;
            yield return null;
            blackIMG.color = new Color(0f, 0f, 0f, fadeValue);
        }

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

        IsFinished = true;
    }
}
