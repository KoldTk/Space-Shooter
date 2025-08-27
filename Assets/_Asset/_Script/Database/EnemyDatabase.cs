using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Database/Create enemy", fileName = "New enemy")]
public class EnemyDatabase : ScriptableObject
{
    public EnemyData[] enemyDatas;
    public EnemyData this[int index] => enemyDatas[index];
}

[System.Serializable]
public class EnemyData
{
    public string enemyName;
    public string ID;
    public GameObject prefab;
    public float health;
    public float movementSpeed;
}
