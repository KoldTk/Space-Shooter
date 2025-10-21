using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
    public int explodeEffectID;
    public int hitEffectID;
    public int maxHP;
    public int currentHP;
    private void Awake()
    {
        currentHP = maxHP;
    }
    public virtual void Die()
    {
        var explosion = EffectPool.Instance.GetPrefab(explodeEffectID, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    public virtual void TakeDamage(int damage)
    {
        if (currentHP > 1)
        {
            var hitEffect = EffectPool.Instance.GetPrefab(hitEffectID, transform.position, transform.rotation);
            currentHP -= damage;
            return;
        }
        
        if (currentHP <= 1)
        {
            Die();
        }
    }  
}
