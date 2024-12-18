using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject explodeEffectPreFab;
    private int enemyHP;
    // Start is called before the first frame update
    void Start()
    {
        enemyHP = 3;
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemyHP -= 1;
        if (enemyHP == 0)
        {
            Die();
        }
    }
    void Die()
    {
        var explosion = Instantiate(explodeEffectPreFab, transform.position, transform.rotation);
        Destroy(gameObject);
    }    
}
