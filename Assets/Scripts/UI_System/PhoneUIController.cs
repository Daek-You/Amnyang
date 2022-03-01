using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PhoneUIController : MonoBehaviour
{
    public GameObject warehouseKey;
    public GameObject shrineKey;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        HasKey();
    }

    void HasKey()
    {
        if (GameDataManager.Instance.hasWarehouseKey == true)
        {
            warehouseKey.gameObject.SetActive(true);
        }
        else
        {
            warehouseKey.gameObject.SetActive(false);
        }

        if (GameDataManager.Instance.hasShrineKey == true)
        {
            shrineKey.gameObject.SetActive(true);
        }
        else
        {
            shrineKey.gameObject.SetActive(false);
        }
    }
}
