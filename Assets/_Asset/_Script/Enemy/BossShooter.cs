using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FireMode
{
    Normal,
    Spiral,
    Wave,
}
public class BossShooter : MonoBehaviour
{
    [Header("General Setting")]
    public FireMode fireMode;
    public Transform firePoint;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private int bulletID;
    private float fireTimer;

    [Header("Wave Settings")]
    [SerializeField] private int bulletsPerWave = 5;
    [SerializeField] float spreadAngle = 45f;

    [Header("Rotate Setting")]
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private float rotationDirection = -360f;
    private void OnEnable()
    {
        ShooterRotate();
    }
    private void OnDisable()
    {
        transform.DOKill();
    }
    void Update()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            fireTimer = 0f;
            Shooting();
        }
    }
    private void Shooting()
    {
        switch (fireMode)
        {
            case FireMode.Normal:
                ShootNormal();
                break;
            case FireMode.Spiral:
                ShootInStarWave();
                break;
            case FireMode.Wave:
                ShootInWave();
                break;
            default:
                break;
        }
    }
    private void ShooterRotate()
    {
        transform.DORotate(new Vector3(0, 0, rotationDirection), 360f / rotationSpeed, RotateMode.FastBeyond360)
         .SetEase(Ease.Linear)
         .SetLoops(-1, LoopType.Restart);
    }
    private void ShootInWave()
    {
        float baseAngle = transform.eulerAngles.z;
        //Shoot Sprial at the same time
        float angleStep = spreadAngle / (bulletsPerWave - 1);
        float startAngle = baseAngle - spreadAngle / 2f;

        for (int i = 0; i < bulletsPerWave; i++)
        {
            float angle = startAngle + angleStep * i;
            Vector3 shootDir = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad),
                                      Mathf.Sin(angle * Mathf.Deg2Rad),
                                      0);
            GameObject bullet = BulletPool.Instance.GetPrefab(bulletID, firePoint.position, firePoint.rotation);
            bullet.GetComponent<EnemyNormalBullet>().Init(shootDir);
        }
    }
    private void ShootNormal()
    {
        GameObject bullet = BulletPool.Instance.GetPrefab(bulletID, firePoint.position, firePoint.rotation);
        Vector3 shootDir = firePoint.right;
        bullet.GetComponent<EnemyNormalBullet>().Init(shootDir);
    }
    private void ShootInStarWave()
    {
        //Shoot Sprial at the same time
        float angleStep = 360f / bulletsPerWave;
        float currentAngle = transform.eulerAngles.z;

        for (int i = 0; i < bulletsPerWave; i++)
        {
            float angle = currentAngle + (angleStep * i);
            Vector3 shootDir = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);

            GameObject bullet = BulletPool.Instance.GetPrefab(bulletID, firePoint.position, firePoint.rotation);
            bullet.GetComponent<EnemyNormalBullet>().Init(shootDir);
        }
    }    
}
