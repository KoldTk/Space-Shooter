using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWavesController : MonoBehaviour
{
    public StageData stageData;
    private int currentWave = 0;

    private void Start()
    {
        StartWave(true);
        //StartCoroutine(RunStageFlow());
    }
    private void OnEnable()
    {
        EventDispatcher<bool>.AddListener(Event.WaveEnd.ToString(), StartWave);
    }
    private void OnDisable()
    {
        
    }
    //private IEnumerator RunStageFlow()
    //{
    //    yield return null;
    //}

    private void StartWave(bool isSpawn)
    {
        if (currentWave < stageData.wavesToSpawn.Count)
        {
            GameObject waveToSpawn = stageData.wavesToSpawn[currentWave];
            Instantiate(waveToSpawn, transform.position, Quaternion.identity, transform);
            currentWave++;
        }
        else
        {
            //Boss Appear
            Debug.Log("Mob Wave End, Boss Appear");
        }    
    }
}
