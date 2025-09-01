using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;


public class SpawnPoint : MonoBehaviour
{
    public EnemyWave[] enemyWave;
    private int currentWave;
    private bool waveEnd = false;

    void Start()
    {
        SpawnEnemyWave();
    }
    private void Update()
    {
        if (!waveEnd)
        {
            CheckEndWave();
        }
    }
    private void OnDisable()
    {
        EventDispatcher<bool>.Dispatch(Event.WaveEnd.ToString(), true);
    }
    private void SpawnEnemyWave()
    {
        var waveInfo = enemyWave[currentWave];
        var startPosition = waveInfo.flyPath[0];
        for (int i = 0; i < waveInfo.numberOfEnemy; i++)
        {
            var enemy = Instantiate(waveInfo.enemyPrefab, startPosition, Quaternion.identity, transform);
            var enemyFlyControl = enemy.GetComponent<FlyPathControl>();
            var enemyInfo = enemy.GetComponent<EnemyHealth>();
            enemyFlyControl.flyPath = waveInfo.flyPath;
            enemyFlyControl.flySpeed = waveInfo.speed;
            enemyInfo.maxHP = waveInfo.enemyHealth;
            startPosition += waveInfo.formationOffset;
        }
        currentWave++;
        if (currentWave < enemyWave.Length)
        {
            Invoke(nameof(SpawnEnemyWave), waveInfo.nextWaveDelay);
        }
    }
    private void CheckEndWave()
    {
        if (currentWave >= enemyWave.Length && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            waveEnd = true;
            gameObject.SetActive(false);
        }
    }    
}

[System.Serializable]
public class EnemyWave
{
    public GameObject enemyPrefab;
    public int numberOfEnemy;
    public Vector3 formationOffset;
    public FlyPath flyPath;
    public int enemyHealth;
    public float speed;
    public float nextWaveDelay;
}

