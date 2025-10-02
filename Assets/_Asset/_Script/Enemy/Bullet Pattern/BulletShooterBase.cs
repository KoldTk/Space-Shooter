using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooterBase : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private int bulletID;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private ShootType shootType;
    [SerializeField] private BulletType bulletType;
    [SerializeField] private float fireRate;
    [SerializeField] private float fireDuration;         //Continously shooting
    [SerializeField] private int shootCount;             //One time shooting

    [Header("Delay And Accelerate Setting")]
    [SerializeField] private float targetSpeed;         //Delay bullet 
    [SerializeField] private float delayTime;           //Delay bullet 
    [SerializeField] private float accelRate;           //Delay bullet

    [Header("Wave Movement Setting")]
    [SerializeField] private float waveAmp;             //Wave bullet
    [SerializeField] private float frequency;           //Wave bullet

    [Header("Pattern Settings")]
    [SerializeField] private PatternType patternType;
    [SerializeField] private RadialPatternConfig radialConfig;
    [SerializeField] private SpiralPatternConfig spiralConfig;
    [SerializeField] private WavePatternConfig waveConfig;
    [SerializeField] private StraightPatternConfig straightConfig;
    [SerializeField] private RandomPatternConfig randomConfig;

    private void OnEnable()
    {
        ChooseShootType(patternType, bulletType);
    }
    //SETUP SHOOTER
    private void ChooseShootType(PatternType pattern, BulletType bulletType)
    {
        switch (shootType)
        {
            case ShootType.Continuous:
                StartCoroutine(FirePatternForSeconds(pattern, fireRate, fireDuration, bulletType));
                break;
            case ShootType.Once:
                StartCoroutine(FirePatternNTimes(pattern, fireRate, shootCount, bulletType));
                break;
        }
    }    
    private void ExecutePattern(PatternType pattern, BulletType bulletType)
    {
        switch (pattern)
        {
            case PatternType.Radial:
                FireRadial(bulletType);
                break;
            case PatternType.Spiral:
                FireSpiral(bulletType);
                break;
            case PatternType.Wave:
                FireWave(bulletType);
                break;
            case PatternType.Straight:
                FireStraight(bulletType);
                break;
            case PatternType.Random:
                FireRandom(bulletType);
                break;
        }
    }
    /// <summary>
    /// Shoot continuously in seconds
    /// </summary>
    private IEnumerator FirePatternForSeconds(PatternType pattern, float fireRate, float duration, BulletType bulletType)
    {
        float timer = 0f;
        while (timer < duration)
        {
            ExecutePattern(pattern, bulletType);
            yield return new WaitForSeconds(fireRate);
            timer += fireRate;
        }
    }
    /// <summary>
    /// Shoot a number of times
    /// </summary>
    private IEnumerator FirePatternNTimes(PatternType pattern, float fireRate, int times, BulletType bulletType)
    {
        for (int i = 0; i < times; i++)
        {
            ExecutePattern(pattern, bulletType);
            yield return new WaitForSeconds(fireRate);
        }
    }
    // ------------ PATTERNS ------------
    private void FireRadial(BulletType bulletType)
    {
        float angleStep = 360f / radialConfig.bulletCount;

        for (int i = 0; i < radialConfig.bulletCount; i++)
        {
            float angle = i * angleStep;
            SpawnBullet(angle, bulletType);
        }
    }
    private void FireSpiral(BulletType bulletType)
    {
        float direction = spiralConfig.clockwise ? -1f : 1f;
        spiralConfig.spiralAngle += spiralConfig.spiralSpeed * direction;
        SpawnBullet(spiralConfig.spiralAngle, bulletType);
    }
    private void FireWave(BulletType bulletType)
    {
        float startAngle = -waveConfig.spreadAngle / 2f;
        float angleStep = waveConfig.spreadAngle / (waveConfig.bulletCount - 1);

        for (int i = 0; i < waveConfig.bulletCount; i++)
        {
            float angle = startAngle + i * angleStep;
            SpawnBullet(angle, bulletType);
        }
    }
    private void FireRandom(BulletType bulletType)
    {
        float halfSpread = randomConfig.spreadAngle / 2f;
        for (int i = 0; i < randomConfig.bulletCount; i++)
        {
            float angle = Random.Range(-halfSpread, halfSpread);
            SpawnBullet(angle, bulletType);
        }
    }
    private void FireStraight(BulletType bulletType)
    {
        for (int i = 0; i < straightConfig.bulletCount; i++)
        {
            SpawnBullet(straightConfig.offset, bulletType);
        }
    }    
    // ------------ UTILS ------------
    private void SpawnBullet(float angle, BulletType bulletType)
    {
        switch (bulletType)
        {
            case BulletType.Normal:
                SpawnNormalBullet(angle);
                break;
            case BulletType.Target:
                SpawnTargetingBullet(angle);
                break;
            case BulletType.Delay:
                SpawnDelayBullet(angle);
                break;
            case BulletType.Wave:
                SpawnWaveBullet(angle);
                break;
        }
    }
    private void SpawnNormalBullet(float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        Vector2 localDir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
        Vector2 worldDir = transform.TransformDirection(localDir);
        GameObject bullet = BulletPool.Instance.GetPrefab(bulletID, transform.position,transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = worldDir * bulletSpeed;
        RotateBulletDirection((Vector2)transform.position + rb.velocity.normalized, bullet);
    }
    private void SpawnTargetingBullet(float angle)
    {
        var player = FindAnyObjectByType<PlayerHealth>();
        if (player != null)
        {
            Transform playerPos = player.transform;
            Vector2 dir = (playerPos.position - transform.position).normalized;
            float newAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.DORotate(new Vector3(0, 0, newAngle), 0)
                .OnComplete(() => SpawnNormalBullet(angle));
        }
    }
    private void SpawnDelayBullet(float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        Vector2 localDir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
        Vector2 worldDir = transform.TransformDirection(localDir);
        GameObject bullet = BulletPool.Instance.GetPrefab(bulletID, transform.position, transform.rotation);
        DelayBullet delayBullet = bullet.GetComponent<DelayBullet>();
        if (delayBullet != null)
        {
            delayBullet.Init(worldDir, bulletSpeed, targetSpeed, accelRate, delayTime);
        }    
    }
    private void SpawnWaveBullet(float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        Vector2 localDir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
        Vector2 worldDir = transform.TransformDirection(localDir);
        GameObject bullet = BulletPool.Instance.GetPrefab(bulletID, transform.position, transform.rotation);
        WaveMovementBullet waveBullet = bullet.GetComponent<WaveMovementBullet>();
        if (waveBullet != null)
        {
            waveBullet.Init(worldDir, bulletSpeed, waveAmp, frequency);
        }
    }    
    private void RotateBulletDirection(Vector2 destination, GameObject bullet)
    {
        Vector2 position = bullet.transform.position; 
        var lookDirection = destination - position; 
        if (lookDirection.magnitude < 0.01f) return; 
        var angle = Vector2.SignedAngle(Vector3.down, lookDirection); 
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
    }    
}

