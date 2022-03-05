using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFoot : MonoBehaviour
{

    public Enemy enemy;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.gameObject.layer == LayerMask.NameToLayer("Foot"))
        {
            enemy.FootSettings(collision);
        }
    }
}
