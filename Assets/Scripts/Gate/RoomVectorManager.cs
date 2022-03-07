using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using LitJson;


public class RoomVectorManager : MonoBehaviour
{
    public static RoomVectorManager Instance { get { return _instance; } }
    private static RoomVectorManager _instance = null;
    private const string RoomVectorPath = "Json/RoomVector";

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }

        DestroyImmediate(gameObject);
    }



    public Vector2 GetRoomVector(string objTag)
    {


        var jsonTextFile = Resources.Load<TextAsset>(RoomVectorPath);
        JsonData jsonData = JsonMapper.ToObject(jsonTextFile.ToString());
        float x = float.Parse(jsonData[0][objTag]["x"].ToString());
        float y = float.Parse(jsonData[0][objTag]["y"].ToString());

        Vector2 roomVector = new Vector2(x, y);
        return roomVector;
    }
}