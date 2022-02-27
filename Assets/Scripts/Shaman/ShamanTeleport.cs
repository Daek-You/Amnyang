using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShamanTeleport : MonoBehaviour
{
    public static ShamanTeleport Instance;
    public Shaman shaman;
    private Vector2 roomVector;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    public void InvokeShamanTeleportToSuji()
    {
        Invoke("ShamanTeleportToSuji", 3.0f);        
    }
    public void ShamanTeleportToSuji()
    {
        roomVector = RoomVectorManager.Instance.GetRoomVector(GameDataManager.Instance.sujiLocation);
        shaman.MoveAnotherRoom(roomVector);
    }

}
