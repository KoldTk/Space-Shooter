using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPointItem : CollectibleManager
{
    [SerializeField] private int value;
    protected override void GainEffect(Collider2D player)
    {
        GameManager.Instance.ManaPointUp(value);
        ItemPool.Instance.ReturnToPool(itemID, gameObject);
    }
}
