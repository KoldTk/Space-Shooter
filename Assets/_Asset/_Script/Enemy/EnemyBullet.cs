using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private int bulletID;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(1);
                BulletPool.Instance.ReturnToPool(bulletID, gameObject);
            }
        }
        if (collision.CompareTag("Delete Zone"))
        {
            BulletPool.Instance.ReturnToPool(bulletID, gameObject);
        }    
    }
}
