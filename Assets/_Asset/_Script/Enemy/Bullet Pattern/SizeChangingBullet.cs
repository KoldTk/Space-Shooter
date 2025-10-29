using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeChangingBullet : MonoBehaviour
{
    [SerializeField] private float startingSize;
    [SerializeField] private float targetSize;
    [SerializeField] private float startingDuration;
    [SerializeField] private float endDuration;
    void Start()
    {
        ChangeSize();
    }
    private void ChangeSize()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(startingSize, startingDuration));
        sequence.Append(transform.DOScale(targetSize, endDuration));
    }    
}
