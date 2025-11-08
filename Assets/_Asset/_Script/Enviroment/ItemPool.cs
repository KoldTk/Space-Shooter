using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : Singleton<ItemPool>
{
    [SerializeField] private int poolSizePerItem = 20;
    [SerializeField] private Transform parentTransform;
    [SerializeField] private ItemDatabase itemDatabase;
    private Dictionary<int, Queue<GameObject>> itemPool;
    protected override void Awake()
    {
        base.Awake();
        PoolInitialize();
    }
    public void PoolInitialize()
    {
        itemPool = new Dictionary<int, Queue<GameObject>>();
        for (int i = 0; i < itemDatabase.itemDatas.Length; i++)
        {
            Queue<GameObject> poolList = new Queue<GameObject>();
            for (int j = 0; j < poolSizePerItem; j++)
            {
                GameObject enemy = Instantiate(itemDatabase.itemDatas[i].itemPrefab, parentTransform);
                enemy.SetActive(false);
                poolList.Enqueue(enemy);
            }
            itemPool.Add(i, poolList);
        }
    }
    public GameObject GetPrefab(int ID, Vector3 position, Quaternion rotation)
    {
        if (!itemPool.ContainsKey(ID)) return null;
        GameObject bullet = null;
        if (itemPool[ID].Count > 0)
        {
            bullet = itemPool[ID].Dequeue();
        }
        else
        {
            bullet = Instantiate(itemDatabase.itemDatas[ID].itemPrefab, position, rotation, parentTransform);
        }
        bullet.transform.SetPositionAndRotation(position, rotation);
        bullet.SetActive(true);
        return bullet;
    }
    public void ReturnToPool(int ID, GameObject bullet)
    {
        bullet.SetActive(false);
        itemPool[ID].Enqueue(bullet);
    }

}
