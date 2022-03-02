using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFoot : MonoBehaviour
{
    private SujiController suji;


    void Start()
    {
       suji = GetComponentInParent<SujiController>();   
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(this.gameObject.layer == LayerMask.NameToLayer("PlayerFoot"))
        {
            suji.FootSettings(collision);
        }
    }
}
