using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Health : MonoBehaviour
{
    public GameObject explodeEffectPrefab;
    public GameObject hitEffectPrefab;
    public int maxHP;
    public int currentHP;
    private void Start()
    {
        currentHP = maxHP;

    }
    public virtual void Die()
    {
        var explosion = Instantiate(explodeEffectPrefab, transform.position, transform.rotation);
        DataManage.score += 1;
        Destroy(gameObject);
    }
    public void TakeDamage(int damage)
    {
        if (currentHP > 0)
        {
            var hitEffect = Instantiate(hitEffectPrefab, transform.position, transform.rotation);
            currentHP -= damage;
            return;
        }
        
        if (currentHP <= 0)
        {
            Die();
        }
    }
}
