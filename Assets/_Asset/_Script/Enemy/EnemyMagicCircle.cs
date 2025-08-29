using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMagicCircle : MonoBehaviour
{
    public Transform firePoint;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private int bulletID;
    [SerializeField] private int bulletsPerWave = 5;
    [SerializeField] float spreadAngle = 45f;
    private float fireTimer;
    
    void OnEnable()
    {
        ShooterRotate();
    }
    private void OnDisable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            fireTimer = 0f;
            ShootInWave();
        }
    }
    private void ShooterRotate()
    {
        transform.DORotate(new Vector3(0, 0, 360), 360f / rotationSpeed, RotateMode.FastBeyond360)
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
    private void Shoot()
    {
        GameObject bullet = BulletPool.Instance.GetPrefab(bulletID, firePoint.position, firePoint.rotation);
        Vector3 shootDir = firePoint.right;
        bullet.GetComponent<EnemyNormalBullet>().Init(shootDir);
    }    
}
