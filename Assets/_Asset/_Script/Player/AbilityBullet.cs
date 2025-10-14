using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBullet : MonoBehaviour
{
    public int gunDamage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))
        {
            BossTakeDmg(collision, gunDamage);
            //Each dmg equal to 10 points
            GameManager.Instance.ScoreUp(gunDamage * 10);
        }
        if (collision.CompareTag("Enemy"))
        {
            EnemyTakeDmg(collision, gunDamage);
            //Each dmg equal to 10 points
            GameManager.Instance.ScoreUp(gunDamage * 10);
        }
        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
        }
    }
    public void EnemyTakeDmg(Collider2D collision, int gunDamage)
    {
        var enemy = collision.GetComponent<EnemyHealth>();
        if ((enemy != null))
        {
            enemy.TakeDamage(gunDamage);
        }
    }
    public void BossTakeDmg(Collider2D collision, int gunDamage)
    {
        var boss = collision.GetComponent<BossControl>();
        if ((boss != null))
        {
            boss.TakeDamage(gunDamage);
        }
    }
}
