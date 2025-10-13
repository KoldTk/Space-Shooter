using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RotateBullet : MonoBehaviour
{
    [SerializeField] private float loopDuration;

    private void OnEnable()
    {
        transform.DORotate(new Vector3(0, 0, 360), loopDuration, RotateMode.FastBeyond360)
        .SetEase(Ease.Linear)
        .SetLoops(-1, LoopType.Restart);
    }
    private void OnDisable()
    {
        transform.DOKill();
    }
}
