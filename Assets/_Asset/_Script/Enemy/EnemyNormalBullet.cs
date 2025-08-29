using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalBullet : EnemyBullet
{
    private Vector3 moveDir;

    public void Init(Vector3 direction)
    {
        moveDir = direction.normalized;
    }

    void Update()
    {
        transform.position += moveDir * bulletSpeed * Time.deltaTime;
    }
}
