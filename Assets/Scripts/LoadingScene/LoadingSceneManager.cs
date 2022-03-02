using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class LoadingSceneManager : MonoBehaviour
{

    [SerializeField] Image progressBar;
    private static string nextScene;
    private WaitForSeconds waitTime = new WaitForSeconds(1.5f);
    
  
    void Start()
    {
        StartCoroutine(LoadSceneCoroutine());
    }

    public static void LoadScene(string sceneName) 
    {
        nextScene = sceneName; 
        SceneManager.LoadScene("Loading_Scene");
    }

    private IEnumerator LoadSceneCoroutine() 
    {

        if(GameDataManager.Instance != null)
        {
            GameDataManager.Instance.isGameStart = false;
        }
        yield return waitTime;

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene); 
        op.allowSceneActivation = false; 
        float timer = 0f;

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
                    op.allowSceneActivation = true;

                    if (GameDataManager.Instance != null)
                    {
                        GameDataManager.Instance.isGameStart = true;
                    }
                    yield break; 
                } 
            } 
        }

    }
}
