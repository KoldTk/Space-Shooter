using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public override void Die()
    {
        base.Die();
        var worldPoint = Camera.main.ScreenToWorldPoint(transform.position);
        Time.timeScale = 0;
        Debug.Log("Player die");
    }
}
