using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{

    public static GameDataManager Instance { get { return _instance; } }
    private static GameDataManager _instance = null;
    public bool isGameStart = false;

    public bool hasShrineKey { get; set; } = false;
    public bool hasWarehouseKey { get; set; } = false;


    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }

        DestroyImmediate(this.gameObject);
    }


    private void Update()
    {
        if (isGameStart)
        {
            FadeInOutController.Instance.FadeIn(false);
            isGameStart = false;
        }
    }

    public void GetKey(string tag)
    {
        if (tag.Equals("Shaman_WarehouseDoor"))
            hasWarehouseKey = true;

        else if (tag.Equals("Shrine"))
            hasShrineKey = true;
    } 

    public bool IsKeyOn(string tag)
    {

        switch (tag)
        {
            case "Shaman_WarehouseDoor":
                return hasWarehouseKey;

            case "Shrine":
                return hasShrineKey;
        }

        return false;
    }
}
