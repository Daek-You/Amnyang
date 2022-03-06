using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    public void ReStart()
    {
        LoadingSceneManager.LoadScene("Start_Scene");
    }

}
