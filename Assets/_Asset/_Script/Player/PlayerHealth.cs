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
        base.Die();
        var worldPoint = Camera.main.ScreenToWorldPoint(transform.position);
        Time.timeScale = 0;
        Debug.Log("Player die");
    }
}
