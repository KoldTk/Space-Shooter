using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobPool : Singleton<MobPool>
{
    [SerializeField] private int poolSizePerEnemy = 20;
    [SerializeField] private Transform parentTransform;
    [SerializeField] private EnemyDatabase enemyDatabase;
    private Dictionary<int, Queue<GameObject>> enemyPool;

    void Awake()
    {
        PoolInitialize();
    }
    public void PoolInitialize()
    {
        enemyPool = new Dictionary<int, Queue<GameObject>>();
        for (int i = 0; i < enemyDatabase.enemyDatas.Length; i++)
        {
            Queue<GameObject> poolList = new Queue<GameObject>();
            for ( int j = 0; j < poolSizePerEnemy; j++)
            {
                GameObject enemy = Instantiate(enemyDatabase.enemyDatas[i].prefab, parentTransform);
                enemy.SetActive(false);
                poolList.Enqueue(enemy);
            }
            enemyPool.Add(i, poolList);
        }
    }
    public GameObject GetPrefab(int ID, Vector3 position, Quaternion rotation)
    {
        if (!enemyPool.ContainsKey(ID)) return null;
        GameObject enemy = null;
        if (enemyPool[ID].Count > 0)
        {
            enemy = enemyPool[ID].Dequeue();
        }
        else
        {
            enemy = Instantiate(enemyDatabase.enemyDatas[ID].prefab, parentTransform);
        }
        enemy.transform.SetPositionAndRotation(position, rotation);
        enemy.SetActive(true);
        return enemy;
    }
    public void ReturnToPool(int ID, GameObject enemy)
    {
        enemy.SetActive(false);
        enemyPool[ID].Enqueue(enemy);
    }    
}
