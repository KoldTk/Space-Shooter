using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
    public GameObject explodeEffectPrefab;
    public GameObject hitEffectPrefab;
    public int maxHP;
    public int currentHP;
    private void Awake()
    {
        currentHP = maxHP;
    }
    public virtual void Die()
    {
        var explosion = Instantiate(explodeEffectPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    public virtual void TakeDamage(int damage)
    {
        if (currentHP > 1)
        {
            var hitEffect = Instantiate(hitEffectPrefab, transform.position, transform.rotation);
            currentHP -= damage;
            return;
        }
        
        if (currentHP <= 1)
        {
            Die();
        }
    }  
}
