using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBullet : BulletMovement
{
    private int lightningDmg;
    private void OnEnable()
    {
        lightningDmg = (int)GetLightningDmg();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyTakeDmg(collision, lightningDmg);
            //Each dmg equal to 10 points
            GameManager.Instance.ScoreUp(lightningDmg * 10);
        }
        if (collision.CompareTag("Boss"))
        {
            BossTakeDmg(collision, lightningDmg);
            //Each dmg equal to 10 points
            GameManager.Instance.ScoreUp(lightningDmg * 10);
        }
        if (collision.CompareTag("Delete Zone"))
        {
            BulletPool.Instance.ReturnToPool(bulletID, gameObject);
        }
    }
    private float GetLightningDmg()
    {
        float dmg;
        switch (GameManager.Instance.powerStage)
        {
            case 1: 
                dmg = GameManager.Instance.playerPower * 0.8f;
                transform.localScale = new Vector3(1.5f, 1.5f, 0);
                return dmg;
            case 2:
                dmg = GameManager.Instance.playerPower * 0.8f;
                transform.localScale = new Vector3(2f, 2f, 0);
                return dmg;
            case 3:
                dmg = GameManager.Instance.playerPower * 0.9f;
                transform.localScale = new Vector3(2, 2, 0);
                return dmg;
            case 4:
                dmg = GameManager.Instance.playerPower * 0.9f;
                transform.localScale = new Vector3(2, 2, 0);
                return dmg;
            default:
                dmg = GameManager.Instance.playerPower;
                transform.localScale = new Vector3(2.5f, 2.5f, 0);
                return dmg;
        }
    }    
}
