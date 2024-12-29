using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyPath : MonoBehaviour
{
    public Waypoint[] wayPoints;
    private void Reset()
    {
        wayPoints = GetComponentsInChildren<Waypoint>();
    }

    private void OnDrawGizmos()
    {
        if (wayPoints == null) return;


        Gizmos.color = Color.yellow;
        for (int i = 0; i < wayPoints.Length - 1; i++)
        {
            Gizmos.DrawLine(wayPoints[i].transform.position, wayPoints[i+1].transform.position);
        }    
    }

    public Vector3 this[int index] => wayPoints[index].transform.position;
}
