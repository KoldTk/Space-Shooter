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
            Debug.Log("Player die");
        }    
    }
    public override void Die()
    {
        GameManager.Instance.playerLives--;
        base.Die();
        Debug.Log("Player die");
    }
}
