using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardPointItem : CollectibleManager
{
    [SerializeField] private int value;
    protected override void GainEffect(Collider2D player)
    {
        GameManager.Instance.RewardPointUp(value);
        Destroy(gameObject, 0.03f);
    }
}
