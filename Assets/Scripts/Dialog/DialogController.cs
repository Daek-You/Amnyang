using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using LitJson;

public class DialogController : MonoBehaviour
{
    public static DialogController Instance = null;
    public Text dialog;
    public Image dialogBackground;
    public bool isDialog = false;
    private string dialogPath = "/Assets/Resources/Json/DialogList.json";

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void ShowDialog(System.String objTag)
    {

        if (File.Exists(Application.dataPath + dialogPath))
        {
            if (isDialog == true)
            {   
                dialogBackground.gameObject.SetActive(true);

                string JsonString = File.ReadAllText(Application.dataPath + dialogPath);

                JsonData jsonData = JsonMapper.ToObject(JsonString);

                dialog.text = jsonData[0][0][objTag].ToString();
            }
            else
            {
                dialogBackground.gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.Log("에러 : 파일을 찾지 못했습니다.");
        }
    }
}
