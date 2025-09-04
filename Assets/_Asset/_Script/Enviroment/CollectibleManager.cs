using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectibleManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Player_Invi"))
        {
            GainEffect(collision);
            //Add 1000 score for each item
            GameManager.Instance.ScoreUp(1000);
        }
        if (collision.CompareTag("Delete Zone"))
        {
            Destroy(gameObject);
        }    
    }
    protected abstract void GainEffect(Collider2D player);
}
