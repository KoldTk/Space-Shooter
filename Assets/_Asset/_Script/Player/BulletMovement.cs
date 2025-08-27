using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private int bulletID;
    public float bulletSpeed;
    private int gunDamage;
    // Start is called before the first frame update
    void Start()
    {
        gunDamage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.GetComponent<EnemyHealth>();
        if ((enemy != null))
        {
            enemy.TakeDamage(gunDamage);
        }
        BulletPool.Instance.ReturnToPool(bulletID, gameObject);
    }
}
