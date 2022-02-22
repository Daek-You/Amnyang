using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using LitJson;

public class DialogCont : MonoBehaviour
{
    public static DialogCont Instance = null;
    public Text dialog;
    public bool isDialog = false;
    private string dialogPath = "/Resources/Json/DialogList.json";
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator FadeDialogInAndOut(string objTag)
    {
        if (File.Exists(Application.dataPath + dialogPath))
        {
            string JsonString = File.ReadAllText(Application.dataPath + dialogPath);

            JsonData jsonData = JsonMapper.ToObject(JsonString);

            dialog.text = jsonData[0][objTag].ToString();

            Debug.Log(dialog.text);

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
        else
        {
            Debug.Log("에러 : 파일을 찾지 못했습니다.");
        }
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

        if (File.Exists(Application.dataPath + dialogPath))
        {
            string JsonString = File.ReadAllText(Application.dataPath + dialogPath);

            JsonData jsonData = JsonMapper.ToObject(JsonString);

            dialog.text = jsonData[0][objTag+isDoorOpenToString].ToString();

            Debug.Log(dialog.text);

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
        else
        {
            Debug.Log("에러 : 파일을 찾지 못했습니다.");
        }
    }

    public void ShowDialog(string objTag)
    {
        StartCoroutine(FadeDialogInAndOut(objTag));
    }

    public void ShowDialog(string objTag, bool isDoorOpen)
    {
        StartCoroutine(FadeDialogInAndOut(objTag, isDoorOpen));
    }

}