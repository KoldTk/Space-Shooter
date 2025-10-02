using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMovementBullet : MonoBehaviour
{
    private float startSpeed;
    private float waveAmplitude;      
    private float waveFrequency;     
    private Rigidbody2D rb;
    private Vector2 moveDir;
    private Vector2 perpDir;
    private float currentSpeed;
    private float waveTimer;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        waveTimer += Time.deltaTime * waveFrequency;
        float waveOffset = Mathf.Sin(waveTimer) * waveAmplitude;
        Vector2 velocity = moveDir * currentSpeed + perpDir * waveOffset;
        rb.velocity = velocity;
    }
    public void Init(Vector2 dir, float start, float amplitude, float frequency)
    {
        moveDir = dir.normalized;
        perpDir = new Vector2(-moveDir.y, moveDir.x); // vector vuông góc
        startSpeed = start;
        waveAmplitude = amplitude;
        waveFrequency = frequency;
        currentSpeed = startSpeed;
        waveTimer = 0f;
    }
}
