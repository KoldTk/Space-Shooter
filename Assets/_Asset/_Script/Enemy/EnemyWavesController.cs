using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWavesController : MonoBehaviour
{
    public StageData stageData;
    private int currentWave = 0;
    [SerializeField] private string bossDialogue;
    [SerializeField] private float bossWaitDuration;
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
            StartCoroutine(InitBossDialogue());
            Debug.Log("Mob Wave End, Boss Appear");
        }    
    }
    private IEnumerator InitBossDialogue()
    {
        yield return new WaitForSeconds(bossWaitDuration);
        EventDispatcher<string>.Dispatch(Event.StartDialogue.ToString(), bossDialogue);
    }    
}
