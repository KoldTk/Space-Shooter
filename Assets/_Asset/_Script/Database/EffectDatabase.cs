using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect Database/Create Effect", fileName = "EffectDatabase")]
public class EffectDatabase : ScriptableObject
{
    public EffectData[] effectDatas;
}
[System.Serializable]
public class EffectData
{
    public GameObject dataPrefab;
    public int ID;
}

