using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircle : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90f;
    private void OnEnable()
    {
        ShooterRotate();
    }
    private void OnDisable()
    {
        transform.DOKill();
    }
    private void ShooterRotate()
    {
        transform.DORotate(new Vector3(0, 0, -360), 360f / rotationSpeed, RotateMode.FastBeyond360)
         .SetEase(Ease.Linear)
         .SetLoops(-1, LoopType.Restart);
    }
}
