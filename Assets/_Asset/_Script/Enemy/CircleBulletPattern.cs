using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBulletPattern : BulletPatternBase
{
    public GameObject bulletPrefab;
    public int bulletCount = 10;
    public float bulletSpeed = 5f;

    public override void ExecutePattern(Vector3 position)
    {
        float angleStep = 360f / bulletCount;

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * angleStep;
            Vector3 dir = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
            GameObject bullet = Instantiate(bulletPrefab, position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = dir * bulletSpeed;
        }
    }
}
