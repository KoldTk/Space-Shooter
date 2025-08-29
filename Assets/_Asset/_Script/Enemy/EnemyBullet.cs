using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBullet : MonoBehaviour
{
    public float bulletSpeed;
    public int bulletID;
    private Vector3 playerPos;
    private Vector3 moveDirection;
    private bool reachedTarget = false;
    private void OnEnable()
    {
        var player = FindAnyObjectByType<PlayerHealth>();
        if (player != null)
        {
            playerPos = player.transform.position;
            moveDirection = (playerPos - transform.position).normalized;
        }    
    }
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
    public void MoveTowardPlayer()
    {
        if (playerPos != null)
        {
            if (!reachedTarget) 
            {
                transform.position = Vector3.MoveTowards(transform.position, playerPos, bulletSpeed * Time.deltaTime);
            }
            else
            {
                transform.position += moveDirection * bulletSpeed * Time.deltaTime;
            }
            if (Vector3.Distance(transform.position, playerPos) < 0.01f)
            {
                reachedTarget = true;
            }    
        }
        else
        {
            transform.Translate(Vector3.down * bulletSpeed * Time.deltaTime);
        }
    }   
}
