using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidBossControl : Health
{
    [SerializeField] private Transform startPosition;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float phaseCount;
    [SerializeField] private string bossName;
    [SerializeField] private int itemDropCount = 10;
    [SerializeField] private string midBossEndDialogue;
    private Vector3 appearPosition;
    private bool haveDialogue;
    private void OnEnable()
    {
        gameObject.tag = "Boss_Invi";
        appearPosition = startPosition.position;
        GameManager.Instance.bossInfo.maxHealth = maxHP;
        GameManager.Instance.bossInfo.bossName = bossName;
        phaseCount = GameManager.Instance.bossInfo.phaseCount;
        EventDispatcher<bool>.AddListener(Event.BossChangePhase.ToString(), RefillHealth);
        EventDispatcher<int>.AddListener(Event.UpdateBossHP.ToString(), UpdateBossHP);
        EventDispatcher<bool>.AddListener(Event.DialogueEnd.ToString(), BossRetreat);
        StartCoroutine(MoveToPosition(startPosition.position));
    }

    private void OnDisable()
    {
        EventDispatcher<bool>.RemoveListener(Event.DialogueEnd.ToString(), BossRetreat);
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
            StartCoroutine(MidBossDieSequence());
            EventDispatcher<bool>.Dispatch(Event.BossDie.ToString(), true);
            EventDispatcher<string>.Dispatch(Event.StartDialogue.ToString(), midBossEndDialogue);
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
    private IEnumerator MoveToPosition(Vector3 target)
    {
        while (Vector2.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = target;
        gameObject.tag = "Boss";
    }
    private void RefillHealth(bool isRefilled)
    {
        currentHP = maxHP;
    }
    private IEnumerator MidBossDieSequence()
    {
        yield return MoveToPosition(appearPosition);

    }
    private void BossRetreat(bool IsRetreat)
    {
        Destroy(transform.parent.gameObject);
    }

}
