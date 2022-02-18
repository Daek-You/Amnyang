using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindArea : MonoBehaviour
{
    public Police police;   //°æ°ü

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            police.isFind = true;
            police.isWalk = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            Invoke("MakeWalk", 2f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        police.isFind = false;
        police.isWalk = true;
    }

    public void MakeWalk()
    {
        police.isWalk = true;
    }
}