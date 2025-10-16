using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Database/Create Data", fileName = "New item")]
public class ItemDatabase : ScriptableObject
{
    public ItemData[] itemDatas; 
}

[System.Serializable]
public class ItemData
{
    public GameObject itemPrefab;
    public int ID;
}
