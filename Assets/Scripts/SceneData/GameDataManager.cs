using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{

    public static GameDataManager Instance { get { return _instance; } }
    private static GameDataManager _instance = null;
    public bool hasShrineKey { get; set; } = false;



    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }    
    }







}
