using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShooter : MonoBehaviour
{
    [SerializeField] private GameObject laser;
    void Update()
    {
        Shooting();
    }
    void Shooting()
    {
        if (Input.GetMouseButton(0))
        {
            laser.SetActive(true);
        }
        else 
        {
            laser.SetActive(false);
        }
    }
}
