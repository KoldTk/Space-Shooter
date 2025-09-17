using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControl : Health
{
    [SerializeField] private Transform startPosition;
    [SerializeField] private float moveSpeed;
    public int shockCount = 0;
    private float shockInterval = 0.5f;
    private void Update()
    {
        if (shockInterval <= 0)
        {
            if (shockCount == 0) return;
            InflictShockDamage();
        }
        else
        {
            shockInterval -= Time.deltaTime;
        }
        
    }
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
        EventDispatcher<bool>.Dispatch(Event.StartAfterBossDialogue.ToString(), true);
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
    private void InflictShockDamage()
    {
        //Take damage overtime if shocked
        float dmgInflict = shockCount * GameManager.Instance.playerPower * 0.5f;
        currentHP -= (int)dmgInflict;
    }    
}
