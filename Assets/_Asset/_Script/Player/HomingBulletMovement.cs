using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBulletMovement : MonoBehaviour
{
    public float bulletSpeed;
    [SerializeField] private int bulletID;
    [SerializeField] private int ballDamage;
    [SerializeField] private float disappearTime;
    private GameObject target;
    void Update()
    {
        FindTarget();
        DisappearAfterSeconds(5);
    }
    private void OnEnable()
    {
        target = FindAnyObjectByType<EnemyHealth>()?.gameObject;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(ballDamage);
        }    
        Destroy(gameObject);
    }
    private void FindTarget()
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
    private void DisappearAfterSeconds(float second)
    {
        disappearTime -= Time.deltaTime;
        if (disappearTime <= 0)
        {
            BulletPool.Instance.ReturnToPool(bulletID, gameObject);
            disappearTime = second;
        }
        
    }    
}
