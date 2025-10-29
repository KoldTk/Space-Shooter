using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingShooter : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float interval;
    private float chaseTime = 0;
    private Vector3 target;
    void Update()
    {
        if (chaseTime <= 0)
        {
            //Start finding target position and reset chase time
            target = FindAnyObjectByType<PlayerHealth>().transform.position;
            chaseTime = interval;
        }
        else
        {
            //Going to target when done setup position
            Chasing();
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
}
