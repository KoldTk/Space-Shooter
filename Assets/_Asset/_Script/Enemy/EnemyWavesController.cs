using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWavesController : MonoBehaviour
{
    public StageData stageData;
    private int currentWave = 0;
    private void OnEnable()
    {
        EventDispatcher<bool>.AddListener(Event.WaveEnd.ToString(), StartWave);
        EventDispatcher<bool>.AddListener(Event.GameStart.ToString(), StartWave);
    }
    private void OnDisable()
    {
        EventDispatcher<bool>.RemoveListener(Event.WaveEnd.ToString(), StartWave);
        EventDispatcher<bool>.RemoveListener(Event.GameStart.ToString(), StartWave);
    }

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
            EventDispatcher<bool>.Dispatch(Event.StartBeforeBossDialogue.ToString(), true);
            Debug.Log("Mob Wave End, Boss Appear");
        }    
    }
}
