using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPool : Singleton<EffectPool>
{
    [SerializeField] private int poolSizePerItem = 20;
    [SerializeField] private Transform parentTransform;
    [SerializeField] private EffectDatabase effectDatabase;
    private Dictionary<int, Queue<GameObject>> effectPool;
    protected override void Awake()
    {
        base.Awake();
        PoolInitialize();
    }
    public void PoolInitialize()
    {
        effectPool = new Dictionary<int, Queue<GameObject>>();
        for (int i = 0; i < effectDatabase.effectDatas.Length; i++)
        {
            Queue<GameObject> poolList = new Queue<GameObject>();
            for (int j = 0; j < poolSizePerItem; j++)
            {
                GameObject enemy = Instantiate(effectDatabase.effectDatas[i].dataPrefab, parentTransform);
                enemy.SetActive(false);
                poolList.Enqueue(enemy);
            }
            effectPool.Add(i, poolList);
        }
    }
    public GameObject GetPrefab(int ID, Vector3 position, Quaternion rotation)
    {
        if (!effectPool.ContainsKey(ID)) return null;
        GameObject bullet = null;
        if (effectPool[ID].Count > 0)
        {
            bullet = effectPool[ID].Dequeue();
        }
        else
        {
            bullet = Instantiate(effectDatabase.effectDatas[ID].dataPrefab, position, rotation, parentTransform);
        }
        bullet.transform.SetPositionAndRotation(position, rotation);
        bullet.SetActive(true);
        return bullet;
    }
    public void ReturnToPool(int ID, GameObject bullet)
    {
        bullet.SetActive(false);
        effectPool[ID].Enqueue(bullet);
    }

}
