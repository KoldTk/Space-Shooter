using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusLivesItem : CollectibleManager
{
    protected override void GainEffect(Collider2D player)
    {
        GameManager.Instance.LivesUp(1);
        ItemPool.Instance.ReturnToPool(itemID, this.gameObject);
    }
}
