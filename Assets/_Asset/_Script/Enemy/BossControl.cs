using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControl : Health
{
    [SerializeField] private Transform startPosition;
    [SerializeField] private float moveSpeed;
    private void OnEnable()
    {
        StartCoroutine(MoveToPosition(startPosition));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(maxHP);
            Die();
        }
    }
    public override void Die()
    {
        base.Die();
    }
    private IEnumerator MoveToPosition(Transform target)
    {
        while (Vector2.Distance(transform.position, target.position) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = target.position;
        EventDispatcher<bool>.Dispatch(Event.BossStartAttack.ToString(), true);
    }
}
