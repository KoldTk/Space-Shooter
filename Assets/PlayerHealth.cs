using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{

    public override void Die()
    {
        DataManage.isAlive = false;
        base.Die();
        var worldPoint = Camera.main.ScreenToWorldPoint(transform.position);
        Time.timeScale = 0;
        Debug.Log("Player die");
    }
}
