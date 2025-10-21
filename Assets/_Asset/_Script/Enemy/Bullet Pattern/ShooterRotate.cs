using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterRotate : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float angle;
    void Start()
    {
        transform.DORotate(new Vector3 (0, 0 , angle), duration, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.Linear);
    }
}
