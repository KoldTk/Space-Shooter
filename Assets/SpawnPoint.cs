using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject enemyPrefab;
    private Vector3 spawnPoint;
    public float spawnTime;
    private float lastSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        spawnPoint = new Vector3(Random.Range(-2.5f, 2.5f), transform.position.y, 0);
        if (lastSpawnTime >= spawnTime)
        {
            Instantiate(enemyPrefab, transform.position + spawnPoint, transform.rotation);
            lastSpawnTime = 0;
        }
        else if (lastSpawnTime < spawnTime)
        {
            lastSpawnTime += Time.deltaTime;
        }
    }
}
