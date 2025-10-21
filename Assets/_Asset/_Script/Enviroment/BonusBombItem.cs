using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBombItem : CollectibleManager
{
    protected override void GainEffect(Collider2D player)
    {
        GameManager.Instance.BombUp(1);
        ItemPool.Instance.ReturnToPool(itemID, this.gameObject);
    }
}
