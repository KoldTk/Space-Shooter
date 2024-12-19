using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunMovement : MonoBehaviour
{
    public float bulletSpeed;
    private int gunDamage;
    // Start is called before the first frame update
    void Start()
    {
        gunDamage = 1;
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
        var enemy = collision.GetComponent<EnemyHealth>();
        if ((enemy != null))
        {
            enemy.TakeDamage(gunDamage);
        }
        Destroy(gameObject);
    }
}
