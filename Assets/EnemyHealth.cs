using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public override void Die()
    {
        base.Die();
        DataManage.enemyCount -= 1;
        if (DataManage.enemyCount <= 0)
        {
            DataManage.isEndGame = true;
            var worldPoint = Camera.main.ScreenToWorldPoint(transform.position);
            Time.timeScale = 0;
        }
        Debug.Log("Enemy Die");
    }
}
