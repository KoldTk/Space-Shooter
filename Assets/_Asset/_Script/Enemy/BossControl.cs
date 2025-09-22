using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class BossControl : Health
{
    [SerializeField] private Transform startPosition;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float phaseCount;
    [SerializeField] private string bossName;
    private void OnEnable()
    {
        GameManager.Instance.bossInfo.maxHealth = maxHP;
        GameManager.Instance.bossInfo.name = bossName;
        phaseCount = GameManager.Instance.bossInfo.phaseCount;
        StartCoroutine(MoveToPosition(startPosition));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(maxHP);
        }
    }
    public override void Die()
    {
        if (phaseCount > 0)
        {
            phaseCount--;
            EventDispatcher<bool>.Dispatch(Event.BossChangePhase.ToString(), true);
            currentHP = maxHP;
        }
        else
        {
            base.Die();
            EventDispatcher<bool>.Dispatch(Event.StartAfterBossDialogue.ToString(), true);
        }
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        EventDispatcher<int>.Dispatch(Event.BossTakeDamage.ToString(), damage);
    }
    private IEnumerator MoveToPosition(Transform target)
    {
        while (Vector2.Distance(transform.position, target.position) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = target.position;
    } 
}
