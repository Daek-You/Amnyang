using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffOnClick : MonoBehaviour
{   
    private GameObject OffOnClickPrefab;

    // Start is called before the first frame update
    void Start()
    {
        OffOnClickPrefab = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        OnClick();
    }

    void OnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Application.Quit();
        }
    }
}
