using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using LitJson;

public class DialogController : MonoBehaviour
{

    public static DialogController Instance { get { return _instance; } }
    public Text dialog;
    public bool isDialog = false;
    private static DialogController _instance = null;
    private const string dialogPath = "Json/DialogList";


    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }

        DestroyImmediate(gameObject);
    }



    private IEnumerator FadeDialogInAndOut(string objTag)
    {

        var jsonTextFile = Resources.Load<TextAsset>(dialogPath); 
        JsonData jsonData = JsonMapper.ToObject(jsonTextFile.ToString());
        dialog.text = jsonData[0][objTag].ToString();
        dialog.color = new Color(dialog.color.r, dialog.color.g, dialog.color.b, 0);

        while (dialog.color.a < 1.0f)
        {
            dialog.color = new Color(dialog.color.r, dialog.color.g, dialog.color.b, dialog.color.a + (Time.deltaTime / 2.0f));
            yield return null;
        }

        while (dialog.color.a > 0.0f)
        {
            dialog.color = new Color(dialog.color.r, dialog.color.g, dialog.color.b, dialog.color.a - (Time.deltaTime / 2.0f));
            yield return null;
        }
        StopCoroutine(FadeDialogInAndOut(objTag));
    }

    private IEnumerator FadeDialogInAndOut(string objTag, bool isDoorOpen)
    {
        string isDoorOpenToString;

        if (!isDoorOpen)
        {
            isDoorOpenToString = "1";
        }
        else
        {
            isDoorOpenToString = "2";
        }


        var jsonTextFile = Resources.Load<TextAsset>(dialogPath);          // Resource 폴더에 있는 JSON 파일을 로드해줘야함
        JsonData jsonData = JsonMapper.ToObject(jsonTextFile.ToString());

        dialog.text = jsonData[0][objTag+isDoorOpenToString].ToString();
        dialog.color = new Color(dialog.color.r, dialog.color.g, dialog.color.b, 0);
        
        while (dialog.color.a < 1.0f)
        {
            dialog.color = new Color(dialog.color.r, dialog.color.g, dialog.color.b, dialog.color.a + (Time.deltaTime / 2.0f));
            yield return null;
        }

        while (dialog.color.a > 0.0f)
        {
            dialog.color = new Color(dialog.color.r, dialog.color.g, dialog.color.b, dialog.color.a - (Time.deltaTime / 2.0f));
            yield return null;
        }

        StopCoroutine(FadeDialogInAndOut(objTag));
    }

    public void ShowDialog(string objTag)
    {
        StopAllCoroutines();
        StartCoroutine(FadeDialogInAndOut(objTag));
    }

    public void ShowDialog(string objTag, bool isDoorOpen)
    {
        StopAllCoroutines();
        StartCoroutine(FadeDialogInAndOut(objTag, isDoorOpen));
    }
}