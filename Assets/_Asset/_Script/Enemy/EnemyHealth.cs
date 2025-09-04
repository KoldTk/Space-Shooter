using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(maxHP);
            Die();
        }
    }
    public override void Die()
    {
        base.Die();
        GameManager.Instance.DropItem(this.transform, 0.75f);
    }  
}
