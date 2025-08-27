using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Health health;
    public Slider playerBar;
    // Start is called before the first frame update
    void Start()
    {

        playerBar.maxValue = health.maxHP;
        UpdateHealthValue();
        health.onHealthChanged += UpdateHealthValue;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void UpdateHealthValue()
    {
        playerBar.value = health.currentHP;
    }
}
