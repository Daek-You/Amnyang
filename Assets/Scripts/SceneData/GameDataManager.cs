using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{

    public static GameDataManager Instance { get { return _instance; } }
    private static GameDataManager _instance = null;
    public bool hasShrineKey { get; set; } = false;

    public SujiController suji;
    public Shaman shaman;
   
    public float sujiCurrentPositionY;
    public float shamanCurrentPositionY;
    public bool isAreTheyInTheSamePlace;
    public string sujiLocation;

    public int warehouseKey = 0;
    public int shrineKey = 0;
    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
    }

    private void Update()
    {
        sujiCurrentPositionY = suji.transform.position.y;
        shamanCurrentPositionY = shaman.transform.position.y;

        if (0 < sujiCurrentPositionY - shamanCurrentPositionY && sujiCurrentPositionY - shamanCurrentPositionY < 15)
            isAreTheyInTheSamePlace = true;
        else
            isAreTheyInTheSamePlace = false;

        if (-10.0f < sujiCurrentPositionY && sujiCurrentPositionY < 40.0f)
            sujiLocation = "Shaman_OutsideDoor_Hidden(Shaman)";
        else if (-133.0f < sujiCurrentPositionY && sujiCurrentPositionY < -55)
            sujiLocation = "Shaman_MainRoomDoor_Hidden2(Shaman)";
        else if (-315 < sujiCurrentPositionY && sujiCurrentPositionY < -233)
            sujiLocation = "Shaman_WarehouseDoor_Hidden(Shaman)";
        else if (-500 < sujiCurrentPositionY && sujiCurrentPositionY < -414)
            sujiLocation = "Shaman_BathroomDoor_Hidden(Shaman)";
    }

    public void GetKey(string tag)
    {
        if (tag == "Shaman_WarehouseDoor_Hidden")
            warehouseKey = 1;
        else if (tag == "Shrine")
            shrineKey = 1;


    } 

    public bool IsKeyOn(string tag)
    {
        if (tag == "Shaman_WarehouseDoor_Hidden")
        {
            if (warehouseKey == 1)
                return true;
            else
                return false;
        }

        else if (tag == "Shrine")
        {
            if (shrineKey == 1)
                return true;
            else
                return false;
        }

        else
            return false;

    }







}
