using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHellEmitter : MonoBehaviour
{
    public enum PatternType
    {
        Radial,
        Spiral,
        Wave,
    }

    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public float bulletSpeed = 5f;

    [Header("Pattern Settings")]
    public int bulletCount = 12;       // Radial/Wave
    public float spreadAngle = 60f;    // Wave
    public float spiralSpeed = 10f;    // Spiral

    private float spiralAngle;

    // ------------ PUBLIC METHODS ------------

    /// <summary>
    /// Bắn một loạt đạn theo pattern đã chọn.
    /// </summary>
    public void FirePattern(PatternType pattern)
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
            FirePattern(pattern);
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
            FirePattern(pattern);
            yield return new WaitForSeconds(fireRate);
        }
    }

    // ------------ PATTERNS ------------

    private void FireRadial()
    {
        float angleStep = 360f / bulletCount;
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * angleStep;
            SpawnBullet(angle);
        }
    }

    private void FireSpiral()
    {
        spiralAngle += spiralSpeed;
        SpawnBullet(spiralAngle);
    }

    private void FireWave()
    {
        float startAngle = -spreadAngle / 2f;
        float angleStep = spreadAngle / (bulletCount - 1);

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = startAngle + i * angleStep;
            SpawnBullet(angle);
        }
    }

    // ------------ UTILS ------------

    private void SpawnBullet(float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = dir * bulletSpeed;
    }
}
