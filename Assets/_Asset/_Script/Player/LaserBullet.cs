using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : BulletMovement
{
    private int damage;
    [SerializeField] private float hitInterval;
    private float lastBulletTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        damage = GameManager.Instance.playerPower;
        if (collision.CompareTag("Enemy"))
        {
            DealDmgToEnemy(collision, damage);
            //Each dmg equal to 10 points
            GameManager.Instance.ScoreUp(damage * 10);
        }
        if (collision.CompareTag("Boss"))
        {
            DealDmgToBoss(collision, damage);
            //Each dmg equal to 10 points
            GameManager.Instance.ScoreUp(damage * 10);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        damage = GameManager.Instance.playerPower;
        if (lastBulletTime >= hitInterval)
        {
            if (collision.CompareTag("Enemy"))
            {
                DealDmgToEnemy(collision, damage);
                GameManager.Instance.ScoreUp(damage * 10);
            }
            if (collision.CompareTag("Boss"))
            {
                DealDmgToBoss(collision, damage);
                GameManager.Instance.ScoreUp(damage * 10);
            }
            lastBulletTime = 0;
        }
        else if (lastBulletTime < hitInterval)
        {
            lastBulletTime += Time.deltaTime;
        }
    }
    private void DealDmgToEnemy(Collider2D collision, int gunDamage)
    {
        var enemy = collision.GetComponent<EnemyHealth>();
        if ((enemy != null))
        {
            enemy.TakeDamage(gunDamage);
        }
    }
    private void DealDmgToBoss(Collider2D collision, int gunDamage)
    {
        var boss = collision.GetComponent<BossControl>();
        if ((boss != null))
        {
            boss.TakeDamage(gunDamage);
        }
        //Need to check interaction with boss phase later
    }
}
