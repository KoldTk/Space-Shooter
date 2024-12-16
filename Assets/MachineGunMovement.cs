using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunMovement : MonoBehaviour
{
    public float bulletSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var newPosition = transform.position;
        newPosition.y += bulletSpeed * Time.deltaTime;
        transform.position = newPosition;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
