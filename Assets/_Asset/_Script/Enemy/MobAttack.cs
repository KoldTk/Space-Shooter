using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAttack : MonoBehaviour
{
    public BulletWave[] bulletWaves;
    private int currentWave;
    private void OnEnable()
    {
        EventDispatcher<Transform>.AddListener(Event.EnemyAttack.ToString(), EnemyAttacking);
    }
    private void OnDisable()
    {
        EventDispatcher<Transform>.RemoveListener(Event.EnemyAttack.ToString(), EnemyAttacking);
    }
    private void EnemyAttacking(Transform enemy)
    {
        if (enemy != this.transform) return;
        //Spawn Enemy Bullet
        Attack();

    }
    private void Attack()
    {
        var bulletWave = bulletWaves[currentWave];
        var startPosition = transform.position;
        var bullet = BulletPool.Instance.GetPrefab(bulletWave.bulletID, startPosition, Quaternion.identity);
        var bulletMovement = bullet.GetComponent<EnemyBullet>();
        bulletMovement.bulletSpeed = bulletWave.speed;
        currentWave++;
        if (currentWave < bulletWaves.Length)
        {
            Invoke(nameof(Attack), bulletWave.interval);
        }
    }    
}

[System.Serializable]
public class BulletWave
{
    public int bulletID;
    public float interval;
    public float speed;
}
