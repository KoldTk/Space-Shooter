using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayBullet : MonoBehaviour
{
    private float startSpeed;       
    private float targetSpeed;     
    private float accelPercent;
    private float delayTime = 0f;        // Time delay before moving
    private bool isDelayed = true;
    private Rigidbody2D rb;
    private Vector2 moveDir;
    private float currentSpeed;
    private float delayTimer;
    [SerializeField] private bool isHoming = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        // Delay phase
        if (isDelayed)
        {
            delayTimer -= Time.deltaTime;
            if (delayTimer <= 0f)
            {
                isDelayed = false;
                Release();
            }
            else
            {
                currentSpeed = Mathf.Lerp(currentSpeed, 0, accelPercent * Time.deltaTime);
                rb.velocity = moveDir * currentSpeed;
            }
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, accelPercent * Time.deltaTime);
        }
        rb.velocity = moveDir * currentSpeed;
    }
    public void Init(Vector2 dir, float start, float target, float accelRate, float delay)
    {
        moveDir = dir.normalized;
        startSpeed = start;
        targetSpeed = target;
        accelPercent = accelRate;
        delayTime = delay;

        currentSpeed = startSpeed;
        delayTimer = delayTime;
        isDelayed = delayTime > 0f;

        rb.velocity = isDelayed ? Vector2.zero : moveDir * currentSpeed;
    }
    public void Release()
    {
        if (isHoming)
        {
            var player = FindAnyObjectByType<PlayerHealth>();
            if (player != null)
            {
                Transform playerPos = player.transform;
                moveDir = (playerPos.position - transform.position).normalized;
            }
            currentSpeed = startSpeed;
            rb.velocity = moveDir * currentSpeed;
        }
    }
}
