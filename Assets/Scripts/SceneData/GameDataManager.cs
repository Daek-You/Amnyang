using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{

    public static GameDataManager Instance { get { return _instance; } }
    private static GameDataManager _instance = null;

    //public SujiController suji;
    //public Shaman shaman;
   
    public float sujiCurrentPositionY;
    public float shamanCurrentPositionY;
    public bool isAreTheyInTheSamePlace;
    public string sujiLocation;
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


        //sujiCurrentPositionY = suji.transform.position.y;
        //shamanCurrentPositionY = shaman.transform.position.y;

        //if (0 < sujiCurrentPositionY - shamanCurrentPositionY && sujiCurrentPositionY - shamanCurrentPositionY < 15)
        //    isAreTheyInTheSamePlace = true;
        //else
        //    isAreTheyInTheSamePlace = false;

        //if (-10.0f < sujiCurrentPositionY && sujiCurrentPositionY < 40.0f)
        //    sujiLocation = "Shaman_OutsideDoor_Hidden(Shaman)";
        //else if (-133.0f < sujiCurrentPositionY && sujiCurrentPositionY < -55)
        //    sujiLocation = "Shaman_MainRoomDoor_Hidden2(Shaman)";
        //else if (-315 < sujiCurrentPositionY && sujiCurrentPositionY < -233)
        //    sujiLocation = "Shaman_WarehouseDoor_Hidden(Shaman)";
        //else if (-500 < sujiCurrentPositionY && sujiCurrentPositionY < -414)
        //    sujiLocation = "Shaman_BathroomDoor_Hidden(Shaman)";
    }

    public void GetKey(string tag)
    {
        if (tag.Equals("WarehouseKey"))
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
