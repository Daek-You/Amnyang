using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingSceneManager : MonoBehaviour
{
    public static LoadingSceneManager instance;
    public static string nextScene;

    [SerializeField]
    Image progressBar; 

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start() 
    {
        StartCoroutine(LoadScene());
    }

    public static void LoadScene(string sceneName) 
    {
        nextScene = sceneName; 
        SceneManager.LoadScene("Loading_Scene"); 
    }

    IEnumerator LoadScene() 
    { 
        yield return null; 

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene); 

        op.allowSceneActivation = false; 

        float timer = 0.0f; 

        while (!op.isDone) 
        { 
            yield return null; 
            
            timer += Time.deltaTime; 

            if (op.progress < 0.9f) 
            { 
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer); 

                if (progressBar.fillAmount >= op.progress) 
                { 
                    timer = 0f; 
                } 
            } 
            else 
            { 
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
                
                if (progressBar.fillAmount == 1.0f) 
                {
                    op.allowSceneActivation = true; yield break; 
                } 
            } 
        } 
    }


    /*
    public Slider slider;
    public string SceneName = "Village_Scene_Jisoo";
    public static string nextScene;

    private float time;
    public bool isReady = false;

    void Start()
    {
        slider.fillRect
        StartCoroutine(LoadScene_());
    }

    public static void LoadScene_()
    {

    }

    public IEnumerator LoadScene_()
    {

        while (time<10.0f)
        {

            time = +Time.time;

            slider.value = time / 10f;

            if (time >= 10.0f)
            {
                L
            }

            yield return null;
        }

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

    }*/
}
