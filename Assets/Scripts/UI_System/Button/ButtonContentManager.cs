using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ButtonContentManager : MonoBehaviour
{

    public static ButtonContentManager Instance { get { return _instance; } }
    private static ButtonContentManager _instance;
    private ButtonContentManager() { }



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


    public void OnClickStartButton()
    {
        LoadingSceneManager.LoadScene("Intro_Scene");
    }

    public void OnClickExitButton()
    {
        Application.Quit();
    }
}
