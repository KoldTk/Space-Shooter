using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyPathControl : MonoBehaviour
{
    [HideInInspector] public FlyPath flyPath;
    public float flySpeed;
    private float initialSpeed;
    private int nextIndex;
    [SerializeField] private GameObject shooter;
    [SerializeField] private bool stayInPlace;
    [SerializeField] private float waitTime;
    private void Start()
    {
        initialSpeed = flySpeed;
    }
    void Update()
    {
        if (flyPath == null)
        {
            return;
        }
        if (nextIndex >= flyPath.wayPoints.Length)
        {
            if (!stayInPlace)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                flySpeed = 0;
                return;
            }
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
            StartCoroutine(AttackSequence());
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
    private IEnumerator AttackSequence()
    {
        flySpeed = 0;
        shooter.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        flySpeed = initialSpeed;
    }    
}
