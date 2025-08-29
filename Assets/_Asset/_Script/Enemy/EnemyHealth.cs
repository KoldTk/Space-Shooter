using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public override void Die()
    {
        base.Die();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(maxHP);
            TakeDamage(maxHP);
            Die();
        }
    }
}
