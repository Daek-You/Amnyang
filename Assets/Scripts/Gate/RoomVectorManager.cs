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
    private const string dialogPath = "/Resources/Json/RoomVector.json";
    public Vector2 roomVector;

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

    private void Update()
    {

    }

    public Vector2 GetRoomVector(string objTag)
    {
        if (File.Exists(Application.dataPath + dialogPath))
        {
            string JsonString = File.ReadAllText(Application.dataPath + dialogPath);

            JsonData jsonData = JsonMapper.ToObject(JsonString);

            float x = float.Parse(jsonData[0][objTag]["x"].ToString());
            float y = float.Parse(jsonData[0][objTag]["y"].ToString());

            roomVector = new Vector2(x, y);

            return roomVector;

        }
        else
        {
            Debug.Log("에러 : 파일을 찾지 못했습니다.");
            return roomVector;
        }
    }
}