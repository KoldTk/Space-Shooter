using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletHellEmitter : MonoBehaviour
{
    public enum PatternType
    {
        Radial,
        Spiral,
        Wave,
    }
    public enum ShootType
    {
        Continuous,
        Once,
    }
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public int bulletID;
    public float bulletSpeed = 5f;
    public ShootType shootType;
    public float fireRate;
    public float fireDuration;         //Continously shooting
    public int shootCount;             //One time shooting

    [Header("Pattern Settings")]
    public PatternType patternType;
    public RadialPatternConfig radialConfig;
    public SpiralPatternConfig spiralConfig;
    public WavePatternConfig waveConfig;

    private void OnEnable()
    {
        ChooseShootType(patternType);
    }
    //SETUP SHOOTER
    public void ChooseShootType(PatternType pattern)
    {
        switch (shootType)
        {
            case ShootType.Continuous:
                StartCoroutine(FirePatternForSeconds(pattern, fireRate, fireDuration));
                break;
            case ShootType.Once:
                StartCoroutine(FirePatternNTimes(pattern, fireRate, shootCount));
                break;
        }
    }    
    public void ExecutePattern(PatternType pattern)
    {
        switch (pattern)
        {
            case PatternType.Radial:
                FireRadial();
                break;
            case PatternType.Spiral:
                FireSpiral();
                break;
            case PatternType.Wave:
                FireWave();
                break;
        }
    }
    /// <summary>
    /// Shoot continuously in seconds
    /// </summary>
    public IEnumerator FirePatternForSeconds(PatternType pattern, float fireRate, float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            ExecutePattern(pattern);
            yield return new WaitForSeconds(fireRate);
            timer += fireRate;
        }
    }
    /// <summary>
    /// Shoot a number of times
    /// </summary>
    public IEnumerator FirePatternNTimes(PatternType pattern, float fireRate, int times)
    {
        for (int i = 0; i < times; i++)
        {
            ExecutePattern(pattern);
            yield return new WaitForSeconds(fireRate);
        }
    }
    // ------------ PATTERNS ------------
    private void FireRadial()
    {
        float angleStep = 360f / radialConfig.bulletCount;

        for (int i = 0; i < radialConfig.bulletCount; i++)
        {
            float angle = i * angleStep;
            SpawnBullet(angle);
        }
    }
    private void FireSpiral()
    {
        float direction = spiralConfig.clockwise ? -1f : 1f;
        spiralConfig.spiralAngle += spiralConfig.spiralSpeed * direction;
        SpawnBullet(spiralConfig.spiralAngle);
    }
    private void FireWave()
    {
        float startAngle = -waveConfig.spreadAngle / 2f;
        float angleStep = waveConfig.spreadAngle / (waveConfig.bulletCount - 1);

        for (int i = 0; i < waveConfig.bulletCount; i++)
        {
            float angle = startAngle + i * angleStep;
            SpawnBullet(angle);
        }
    }
    // ------------ UTILS ------------
    private void SpawnBullet(float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        Vector2 localDir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

        Vector2 worldDir = transform.TransformDirection(localDir);
        GameObject bullet = BulletPool.Instance.GetPrefab(bulletID, transform.position, transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = worldDir * bulletSpeed;
    }
}
[System.Serializable]
public class RadialPatternConfig
{
    public int bulletCount;
}
[System.Serializable]
public class SpiralPatternConfig
{
    public float spiralAngle;
    public float spiralSpeed;
    public bool clockwise;
}
[System.Serializable]
public class WavePatternConfig
{
    public int bulletCount;
    public float spreadAngle;
}
