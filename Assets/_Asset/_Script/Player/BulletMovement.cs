using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public int bulletID;
    public float bulletSpeed;
    private int gunDamage;
    void Update()
    {
        transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        gunDamage = GameManager.Instance.playerPower;
        if (collision.CompareTag("Enemy"))
        {
            EnemyTakeDmg(collision, gunDamage);
            //Each dmg equal to 10 points
            GameManager.Instance.ScoreUp(gunDamage * 10);
        }
        if (collision.CompareTag("Boss"))
        {
            BossTakeDmg(collision, gunDamage);
            //Each dmg equal to 10 points
            GameManager.Instance.ScoreUp(gunDamage * 10);
        }
        if (collision.CompareTag("Delete Zone"))
        {
            BulletPool.Instance.ReturnToPool(bulletID, gameObject);
        }
    }
    public void EnemyTakeDmg(Collider2D collision, int gunDamage)
    {
        var enemy = collision.GetComponent<EnemyHealth>();
        if ((enemy != null))
        {
            enemy.TakeDamage(gunDamage);   
        }
        BulletPool.Instance.ReturnToPool(bulletID, gameObject);
    }
    public void BossTakeDmg(Collider2D collision, int gunDamage)
    {
        var boss = collision.GetComponent<BossControl>();
        if ((boss != null))
        {
            boss.TakeDamage(gunDamage); 
        }
        BulletPool.Instance.ReturnToPool(bulletID, gameObject);
    }
}
