using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPointItem : CollectibleManager
{
    [SerializeField] private int point;
    private Transform player;
    protected override void GainEffect(Collider2D player)
    {
        GameManager.Instance.ScoreUp(point);
        ItemPool.Instance.ReturnToPool(itemID, this.gameObject);
    }
    private void Update()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerControl>().transform;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, 10 * Time.deltaTime);
        }
    }
}
