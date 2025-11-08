using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : Singleton<BulletPool>
{
    //Pool contains bullets that used in-game
    [SerializeField] private int poolSizePerEnemy = 50;
    [SerializeField] private Transform parentTransform;
    [SerializeField] private BulletDatabase bulletDatabase;
    private Dictionary<int, Queue<GameObject>> enemyPool;
    protected override void Awake()
    {
        base.Awake();
        PoolInitialize();
    }
    public void PoolInitialize()
    {
        enemyPool = new Dictionary<int, Queue<GameObject>>();
        for (int i = 0; i < bulletDatabase.bulletDatas.Length; i++)
        {
            Queue<GameObject> poolList = new Queue<GameObject>();
            for (int j = 0; j < poolSizePerEnemy; j++)
            {
                GameObject enemy = Instantiate(bulletDatabase.bulletDatas[i].prefab, parentTransform);
                enemy.SetActive(false);
                poolList.Enqueue(enemy);
            }
            enemyPool.Add(i, poolList);
        }
    }
    public GameObject GetPrefab(int ID, Vector3 position, Quaternion rotation)
    {
        if (!enemyPool.ContainsKey(ID)) return null;
        GameObject bullet = null;
        if (enemyPool[ID].Count > 0)
        {
            bullet = enemyPool[ID].Dequeue();
        }
        else
        {
            bullet = Instantiate(bulletDatabase.bulletDatas[ID].prefab, position, rotation, parentTransform);
        }
        bullet.transform.SetPositionAndRotation(position, rotation);
        bullet.SetActive(true);
        return bullet;
    }
    public void ReturnToPool(int ID, GameObject bullet)
    {
        bullet.SetActive(false);
        enemyPool[ID].Enqueue(bullet);
    }
}
