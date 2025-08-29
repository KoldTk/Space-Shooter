using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyPathControl : MonoBehaviour
{
    public FlyPath flyPath;
    public float flySpeed;
    private int nextIndex;
    void Update()
    {
        if (flyPath == null)
        {
            return;
        }
        if (nextIndex >= flyPath.wayPoints.Length)
        {
            Destroy(gameObject);
            return;
        }
        if (transform.position != flyPath[nextIndex])
        {
            FlyToNextWaypoint();
            LookAt(flyPath[nextIndex]);
        }
        else
        {
            nextIndex++;
        }

    }

    private void FlyToNextWaypoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, flyPath[nextIndex], flySpeed * Time.deltaTime);
        if (transform.position == flyPath[1])
        {
            EventDispatcher<Transform>.Dispatch(Event.EnemyAttack.ToString(), transform);
        }    
    }

    private void LookAt(Vector2 destination)
    {
        Vector2 position = transform.position;
        var lookDirection = destination - position;
        if (lookDirection.magnitude < 0.01f)
            return;
        var angle = Vector2.SignedAngle(Vector2.down, lookDirection);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
