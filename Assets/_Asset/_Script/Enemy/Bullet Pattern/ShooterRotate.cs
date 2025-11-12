using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterRotate : MonoBehaviour
{
    private enum RotateType
    {
        Restart,
        Yoyo,
    }
    [SerializeField] private float duration;
    [SerializeField] private float angle;
    private LoopType loopType;
    [SerializeField] private RotateType rotateType;
    void Start()
    {
        loopType = GetLoopType();
        transform.DORotate(new Vector3 (0, 0 , angle), duration, RotateMode.FastBeyond360)
            .SetLoops(-1, loopType)
            .SetEase(Ease.Linear);
    }
    private void OnDisable()
    {
        transform.DOKill();
    }
    private void OnDestroy()
    {
        transform.DOKill();
    }
    private LoopType GetLoopType()
    {
        switch (rotateType)
        {
            case RotateType.Restart:
                loopType = LoopType.Restart;
                break;
            case RotateType.Yoyo:
                loopType = LoopType.Yoyo;
                break;
            default:
                break;
        }
        return loopType;
    }    
}

