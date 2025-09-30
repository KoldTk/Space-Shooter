using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBulletPattern : BulletHellEmitter
{
    private void OnEnable()
    {
        ChooseShootType(patternType);
    }
}
