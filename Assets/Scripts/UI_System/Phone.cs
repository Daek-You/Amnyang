using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Phone : MonoBehaviour
{
    private bool isPause;
    public Canvas canvas;
    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        isPause = false;
        image.color = new Color(0, 0, 0, 0.5f);
        canvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Pause();
    }

    private void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause)
            {
                isPause = false;
                ShowPhoneUI();
            }
            else
            {
                isPause = true; 
                ShowPhoneUI();
            }
    }
    }

    private void ShowPhoneUI()
    {
        if (isPause)
        {
            canvas.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            canvas.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
