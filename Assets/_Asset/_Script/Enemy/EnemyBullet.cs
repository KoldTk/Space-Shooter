using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private int bulletID;
    private Collider2D targetCollider;
    private float evadeDistance = 1f;
    private float rewardCooldown = 1f;
    private float lastRewardTime;
    private void OnEnable()
    {
        targetCollider = FindAnyObjectByType<PlayerControl>()?.GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(1);
                BulletPool.Instance.ReturnToPool(bulletID, gameObject);
            }
        }
        if (collision.CompareTag("Delete Zone"))
        {
            BulletPool.Instance.ReturnToPool(bulletID, gameObject);
        }    
    }
    private void Update()
    {
        if (targetCollider == null)
        {
            targetCollider = FindAnyObjectByType<PlayerControl>().GetComponent<Collider2D>();
        }
        else
        {
            //Closest distance from bullet border to player
            Vector2 closest = targetCollider.ClosestPoint(transform.position);
            float distance = Vector2.Distance(transform.position, closest);
            if (Mathf.Abs(distance - evadeDistance) <= 0.01f)
            {
                if (Time.time - lastRewardTime >= rewardCooldown)
                {
                    GameManager.Instance.evadePoint += 0.5f;
                    EventDispatcher<bool>.Dispatch(Event.StatusChange.ToString(), true);
                    lastRewardTime = Time.time;
                }
            }
        }

    }
    public void ChangeToPoint()
    {
        BulletPool.Instance.ReturnToPool(bulletID, gameObject);
        ItemPool.Instance.GetPrefab(6, transform.position, transform.rotation);
    }    
}
