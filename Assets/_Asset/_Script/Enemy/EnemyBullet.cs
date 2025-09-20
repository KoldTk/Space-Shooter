using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBullet : MonoBehaviour
{
    public float bulletSpeed;
    public int bulletID;
    public float rotation;
    private Vector2 spawnPoint;
    private float timer;
    private void OnEnable()
    {
        spawnPoint = new Vector2(transform.position.x, transform.position.y);
    }
    private void OnDisable()
    {
        timer = 0;
    }
    private void Update()
    {
        //timer += Time.deltaTime; 
        //transform.position = Movement(timer);
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
    private Vector2 Movement(float timer)
    {
        float x = timer * bulletSpeed * transform.right.x;
        float y = timer * bulletSpeed * transform.right.y;
        return new Vector2(x + spawnPoint.x, y + spawnPoint.y);
    }    
}
