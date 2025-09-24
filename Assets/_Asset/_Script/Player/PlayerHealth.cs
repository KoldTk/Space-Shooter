using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Bullet"))
        {
            Die();
        }    
    }
    public override void Die()
    {//Need sequence for destroying character
        base.Die();
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }
}
