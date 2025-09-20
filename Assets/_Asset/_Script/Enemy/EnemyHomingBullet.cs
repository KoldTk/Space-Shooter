using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHomingBullet : EnemyBullet
{
    private Vector3 moveDirection;
    private Vector3 playerPos;
    private void OnEnable()
    {
        var player = FindAnyObjectByType<PlayerHealth>();
        if (player != null)
        {
            playerPos = player.transform.position;
            moveDirection = (playerPos - transform.position).normalized;
        }
    }
    public void MoveTowardPlayer()
    {
        if (playerPos != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPos, bulletSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.down * bulletSpeed * Time.deltaTime);
        }
    }
}
