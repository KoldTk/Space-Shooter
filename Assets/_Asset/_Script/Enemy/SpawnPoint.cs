using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnPoint : MonoBehaviour
{
    public EnemyWave[] enemyWave;
    private int currentWave;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave();
    }

    private void SpawnEnemyWave()
    {
        var waveInfo = enemyWave[currentWave];
        var startPosition = waveInfo.flyPath[0];
        for (int i = 0; i < waveInfo.numberOfEnemy; i++)
        {
            var enemy = Instantiate(waveInfo.enemyPrefab, startPosition, Quaternion.identity);
            var enemyFlyControl = enemy.GetComponent<FlyPathControl>();
            enemyFlyControl.flyPath = waveInfo.flyPath;
            enemyFlyControl.flySpeed = waveInfo.speed;
            startPosition += waveInfo.formationOffset;
        }
        currentWave++;
        if (currentWave < enemyWave.Length)
        {
            Invoke(nameof(SpawnEnemyWave), waveInfo.nextWaveDelay);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}

[System.Serializable]
public class EnemyWave
{
    public GameObject enemyPrefab;
    public int numberOfEnemy;
    public Vector3 formationOffset;
    public FlyPath flyPath;
    private Vector3 spawnPoint;
    public float speed;
    public float nextWaveDelay;
}

