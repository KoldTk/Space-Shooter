using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyPathControl : MonoBehaviour
{
    public FlyPath flyPath;
    public float flySpeed;
    private int nextIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (flyPath == null)
        {
            return;
        }
        if (nextIndex >= flyPath.wayPoints.Length)
        {
            return;
        }
        if (transform.position != flyPath[nextIndex])
        {
            FlyToNextWaypoint();
        }
        else
        {
            nextIndex++;
        }

    }

    private void FlyToNextWaypoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, flyPath[nextIndex], flySpeed * Time.deltaTime);
    }
}
