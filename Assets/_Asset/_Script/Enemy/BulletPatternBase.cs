using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletPatternBase : MonoBehaviour
{
    private void OnEnable()
    {
        ExecutePattern();
    }
    public abstract void ExecutePattern();
}
