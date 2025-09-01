using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StageData/NewStage", fileName = "StageNumber")]
public class StageData : ScriptableObject
{
    public List<GameObject> wavesToSpawn;
}
