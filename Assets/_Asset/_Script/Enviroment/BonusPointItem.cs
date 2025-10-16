using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPointItem : CollectibleManager
{
    [SerializeField] private int point;
    protected override void GainEffect(Collider2D player)
    {
        GameManager.Instance.ScoreUp(point);
    }
}
