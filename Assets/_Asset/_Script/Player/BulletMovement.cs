using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public int bulletID;
    public float bulletSpeed;
    public int gunDamage;
    private float milestoneHP = 0.75f; //Boss change to next phase when reach milestone
    void Update()
    {
        transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyTakeDmg(collision);
            //Each dmg equal to 10 points
            GameManager.Instance.ScoreUp(gunDamage * 10);
        }
        if (collision.CompareTag("Boss"))
        {
            BossTakeDmg(collision);
            //Each dmg equal to 10 points
            GameManager.Instance.ScoreUp(gunDamage * 10);
        }
        if (collision.CompareTag("Delete Zone"))
        {
            BulletPool.Instance.ReturnToPool(bulletID, gameObject);
        }
        Debug.Log(GameManager.Instance.playerScore);
    }
    
    private void EnemyTakeDmg(Collider2D collision)
    {
        var enemy = collision.GetComponent<EnemyHealth>();
        if ((enemy != null))
        {
            enemy.TakeDamage(gunDamage);   
        }
        BulletPool.Instance.ReturnToPool(bulletID, gameObject);
    }
    private void BossTakeDmg(Collider2D collision)
    {
        var boss = collision.GetComponent<BossControl>();
        if ((boss != null))
        {
            float hpToNextPhase = boss.maxHP * milestoneHP;
            boss.TakeDamage(gunDamage);
            if (boss.currentHP < (hpToNextPhase))
            {
                EventDispatcher<bool>.Dispatch(Event.BossChangePhase.ToString(), true);
                milestoneHP -= 0.25f;
                milestoneHP = Mathf.Clamp01(milestoneHP);
            }    
        }
        BulletPool.Instance.ReturnToPool(bulletID, gameObject);
    }
}
