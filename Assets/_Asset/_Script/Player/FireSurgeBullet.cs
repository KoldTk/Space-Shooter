using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSurgeBullet : MonoBehaviour
{
    [SerializeField] private int gunDamage;
    [SerializeField] private float liveDuration;
    [SerializeField] private float dmgInterval;
    [SerializeField] private SpriteRenderer sprite;
    private float currentTime;
    private float currentInterval;

    private void OnEnable()
    {
        currentInterval = dmgInterval;
        sprite.DOFade(0.75f, 1);
        transform.DOScale(new Vector3 (1.5f,6,0), 1);
        currentTime = liveDuration;
    }
    private void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            GameManager.Instance.playerUsingSpell = false;
            EventDispatcher<bool>.Dispatch(Event.SpellEnd.ToString(), true);
            Destroy(transform.parent.gameObject);
        }
    }
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
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (currentInterval > 0)
        {
            currentInterval -= Time.deltaTime;
        }
        else
        {
            currentInterval = dmgInterval;
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
    }
    public void EnemyTakeDmg(Collider2D collision, int gunDamage)
    {
        EnemyHealth[] enemies = collision.GetComponents<EnemyHealth>();
        if ((enemies != null))
        {
            foreach (EnemyHealth enemy in enemies)
            {
                enemy.TakeDamage(gunDamage);
            }    
        }
    }
    public void BossTakeDmg(Collider2D collision, int gunDamage)
    {
        BossControl[] bosses = collision.GetComponents<BossControl>();
        if ((bosses != null))
        {
            foreach(BossControl boss in bosses)
            {
                boss.TakeDamage(gunDamage);
            }    
        }
    }
}
