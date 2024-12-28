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
    public System.Action onHealthChanged;
    private void Start()
    {

        currentHP = maxHP;
        onHealthChanged?.Invoke();

    }
    public virtual void Die()
    {
        var explosion = Instantiate(explodeEffectPrefab, transform.position, transform.rotation);
        DataManage.score += 1;
        Destroy(gameObject);
    }
    public void TakeDamage(int damage)
    {
        if (currentHP > 1)
        {
            var hitEffect = Instantiate(hitEffectPrefab, transform.position, transform.rotation);
            currentHP -= damage;
            onHealthChanged?.Invoke();
            return;
        }
        
        if (currentHP <= 1)
        {
            Die();
        }
    }
}
