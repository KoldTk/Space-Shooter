using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBulletMovement : BulletMovement
{
    private GameObject target;
    void Update()
    {
        MoveToTarget();
    }
    private void OnEnable()
    {
        target = FindClosestTarget();
    }
    private GameObject FindClosestTarget()
    {
        EnemyHealth[] enemies = FindObjectsByType<EnemyHealth>(FindObjectsSortMode.None);
        GameObject closestTarget = null;
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
}
