using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteEnemy : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.GetComponent<EnemyHealth>();
        if (enemy != null )
        {
            Destroy(enemy.gameObject);
        }    
    }

}
