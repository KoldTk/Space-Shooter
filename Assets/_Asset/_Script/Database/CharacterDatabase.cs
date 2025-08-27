using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Database/Create character", fileName = "New character")]
public class CharacterDatabase : ScriptableObject
{
    public CharacterData[] characterDatas;
    public CharacterData this[int index] => characterDatas[index];
}

[System.Serializable]
public class CharacterData
{
    public string characterName;
    public string ID;
    public GameObject prefab;

}
