using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using LitJson;
using System.Text;     // StringBuilder



public class Narration_Ending : MonoBehaviour
{

    public Text text;
    private const string jsonFilePath = "Json/Narration_Ending";
    private JsonData _jsonData;
    private int index;
    private StringBuilder _jsonStringBD = new StringBuilder();
    private WaitForSeconds delayTime = new WaitForSeconds(0.08f);
    private WaitForSeconds commaDelayTime = new WaitForSeconds(1.5f);
    private WaitForSeconds loadingDelay = new WaitForSeconds(2f);
    private string jsonDataLine;





    void Awake()
    {
        _jsonData = ReadJsonFile();
        index = 0;
    }


    public void ShowNarration(bool restingComma)
    {
        if (_jsonData != null && index < _jsonData[0].Count)
        {
            text.text = "";
            _jsonStringBD.Clear();
            jsonDataLine = _jsonData[0][index].ToString();
            StartCoroutine(TextTyping(restingComma));
            index++;
        }
    }


    public void Restart()
    {
        LoadingSceneManager.LoadScene("Start_Scene");
    }


    private IEnumerator TextTyping(bool restingComma)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);

        foreach (char i in jsonDataLine)
        {
            if (i == ',' && restingComma)
            {
                text.text = _jsonStringBD.Append(i).ToString();
                yield return commaDelayTime;
                continue;
            }

            text.text = _jsonStringBD.Append(i).ToString();
            yield return delayTime;
        }
    }


    public void TextFadeOut()
    {
        StartCoroutine(FadeOutText());
    }

    private IEnumerator FadeOutText()
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a);

        while (text.color.a > 0f)
        {
            float alphaValue = text.color.a - (Time.deltaTime);
            text.color = new Color(text.color.r, text.color.g, text.color.b, alphaValue);
            yield return null;
        }
    }



    private JsonData ReadJsonFile()
    {
        var jsonTextFile = Resources.Load<TextAsset>(jsonFilePath);
        JsonData jsonData = JsonMapper.ToObject(jsonTextFile.ToString());
        return jsonData;
    }
}
