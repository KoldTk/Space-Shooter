using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Character Database/Create bullet", fileName = "New bullet")]
public class BulletDatabase : ScriptableObject
{
    public BulletData[] bulletDatas;
    public BulletData this[int index] => bulletDatas[index];
}

[System.Serializable]
public class BulletData
{
    public string bulletName;
    public string ID;
    public GameObject prefab;
    public float moveSpeed;
    public float damage;
}
