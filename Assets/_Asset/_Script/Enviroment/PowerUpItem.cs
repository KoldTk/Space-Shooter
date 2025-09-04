using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpItem : CollectibleManager
{
    [SerializeField] private int value;
    protected override void GainEffect(Collider2D player)
    {
        GameManager.Instance.PowerUp(value);
        Destroy(gameObject, 0.03f);
    }
}
