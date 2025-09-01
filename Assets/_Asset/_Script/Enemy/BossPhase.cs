using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhase : MonoBehaviour
{
    [SerializeField] private Queue<Transform> waves = new Queue<Transform>();
    [SerializeField] private float waveTime;

    private void OnEnable()
    {
        foreach (Transform child in transform)
        {
            waves.Enqueue(child);
        }    
    }
    private void Start()
    {
        if (waves.Count > 0)
        {
            Transform wave = waves.Dequeue();
            wave.gameObject.SetActive(true);
        }    
    }
    private void Update()
    {
        CountdownPhase();
    }
    private void CountdownPhase()
    {
        float time = waveTime;
        waveTime -= Time.deltaTime;
        if (waveTime <= 0)
        {
            if (waves.Count > 0)
            {
                Transform wave = waves.Dequeue();
                wave.gameObject.SetActive(true);
                waveTime = time;
            }
        }    
    }    
}
