using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBombMovement : MonoBehaviour
{
    private GameObject target;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private GameObject explodeEffectPrefab;
    private int bombDamage;
    void Update()
    {
        MoveToTarget();
    }
    private void OnEnable()
    {
        target = FindClosestTarget();
    }
    private void OnDisable()
    {
        CreateExplodeEffect();
    }
    private GameObject FindClosestTarget()
    {
        EnemyHealth[] enemies = FindObjectsByType<EnemyHealth>(FindObjectsSortMode.None);
        BossControl boss = FindAnyObjectByType<BossControl>();
        GameObject closestTarget = null;
        if (boss != null)
        {
            closestTarget = boss.gameObject;
        }
        else
        {
            float minDistance = Mathf.Infinity;
            Vector3 currentPos = transform.position;
            foreach (EnemyHealth enemy in enemies)
            {
                float dist = Vector3.Distance(currentPos, enemy.transform.position);
                if (dist < minDistance)
                {
                    minDistance = dist;
                    closestTarget = enemy.gameObject;
                }
            }
        }
        return closestTarget;
    }
    private void MoveToTarget()
    {
        if (target == null)
        {
            transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, bulletSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        bombDamage = GameManager.Instance.playerPower * 5;
        if (collision.CompareTag("Enemy"))
        {
            EnemyTakeDmg(collision, bombDamage);
            //Each dmg equal to 10 points
            GameManager.Instance.ScoreUp(bombDamage * 10);
            Destroy(gameObject);
        }
        if (collision.CompareTag("Boss"))
        {
            BossTakeDmg(collision, bombDamage);
            //Each dmg equal to 10 points
            GameManager.Instance.ScoreUp(bombDamage * 10);
            Destroy(gameObject);
        }
        if (collision.CompareTag("Delete Zone"))
        {
            Destroy(gameObject);
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
            enemy.TakeDamage(bombDamage);
        }
    }
    public void BossTakeDmg(Collider2D collision, int gunDamage)
    {
        var boss = collision.GetComponent<BossControl>();
        if ((boss != null))
        {
            boss.TakeDamage(bombDamage);
        }
    }
    private void CreateExplodeEffect()
    {
        GameObject explosion = Instantiate(explodeEffectPrefab, transform.position, transform.rotation);
        Destroy(explosion, 1.5f);
    }    
}
