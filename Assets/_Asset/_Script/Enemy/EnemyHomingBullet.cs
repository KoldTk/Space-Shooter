using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHomingBullet : EnemyBullet
{
    void Update()
    {
        MoveTowardPlayer();
    }
}
