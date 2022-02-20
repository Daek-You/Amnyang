using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartButton : MonoBehaviour
{
    public GameObject startButton;

    public void MoveScene()
    {
        SceneManager.LoadScene("Loading_Scene");
    }
}
