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
    [SerializeField] private int itemDropCount = 10;
    private void OnEnable()
    {
        GameManager.Instance.bossInfo.maxHealth = maxHP;
        GameManager.Instance.bossInfo.bossName = bossName;
        phaseCount = GameManager.Instance.bossInfo.phaseCount;
        EventDispatcher<bool>.AddListener(Event.BossChangePhase.ToString(), RefillHealth);
        EventDispatcher<int>.AddListener(Event.UpdateBossHP.ToString(), UpdateBossHP);
        StartCoroutine(MoveToPosition(startPosition));
    }
    private void OnDisable()
    {
        EventDispatcher<bool>.RemoveListener(Event.BossChangePhase.ToString(), RefillHealth);
        EventDispatcher<int>.RemoveListener(Event.UpdateBossHP.ToString(), UpdateBossHP);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(GameManager.Instance.bossInfo.maxHealth);
            }
        }    
    }
    public override void Die()
    {
        phaseCount--;
        if (phaseCount > 0)
        {
            EventDispatcher<bool>.Dispatch(Event.BossChangePhase.ToString(), true);
            RefillHealth(true);
            for (int i = 0; i < itemDropCount; i++)
            {
                GameManager.Instance.DropItem(this.transform);
            }    
        }
        else
        {
            GameManager.Instance.StartCoroutine(GameManager.Instance.DeleteBullet());
            base.Die();
            EventDispatcher<bool>.Dispatch(Event.BossDie.ToString(), true);
            EventDispatcher<bool>.Dispatch(Event.StartAfterBossDialogue.ToString(), true);
        }
        EventDispatcher<bool>.Dispatch(Event.SpellEnd.ToString(), true);
    }
    public void UpdateBossHP(int hp)
    {
        maxHP = hp;
        currentHP = maxHP;
        GameManager.Instance.bossInfo.maxHealth = maxHP;
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
    private void RefillHealth(bool isRefilled)
    {
        currentHP = maxHP;
    }    
}
