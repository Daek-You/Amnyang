using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingSceneManager : MonoBehaviour
{
    public Slider slider;
    public string SceneName = "Village_Scene_Jisoo";

    private float time;


    void Start()
    {
        StartCoroutine(LoadScene());
    }

    public IEnumerator LoadScene()
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneName);

        operation.allowSceneActivation = false;



        while (!operation.isDone)
        {

            time = +Time.time;

            slider.value = time / 10f;

            if (time > 10)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }

    }
}
