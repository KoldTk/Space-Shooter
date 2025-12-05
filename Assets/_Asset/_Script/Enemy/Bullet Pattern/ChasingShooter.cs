using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingShooter : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float interval;
    private float chaseTime = 0;
    private Vector3 target;
    private Vector3 startPos;
    private Vector3 moveDir;
    void Update()
    {
        if (chaseTime <= 0)
        {
            //Start finding target position and reset chase time
            target = FindAnyObjectByType<PlayerHealth>().transform.position;
            startPos = transform.position;
            chaseTime = interval;
            moveDir = target - startPos;
        }
        else
        {
            //Going to target when done setup position
            Chasing();
            RotateShooter();
            if (Vector3.Distance(transform.position, target) < 0.1f)
            {
                //Start countdown when reach destination
                chaseTime -= Time.deltaTime;
            }    
        }
    }
    private void Chasing()
    {
        if (target == null) return;
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
    }
    private void RotateShooter()
    {
        if (moveDir.sqrMagnitude < 0.0001f)
            return;
        Vector3 oppositeDir = -moveDir;
        float angle = Mathf.Atan2(oppositeDir.y, oppositeDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }    
}
