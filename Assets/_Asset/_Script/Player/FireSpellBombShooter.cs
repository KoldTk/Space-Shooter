using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpellBombShooter : SpellBombShooter
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int numberOfBullet;
    [SerializeField] private float radius = 1f;
    public override IEnumerator StartSpell()
    {
        float angleStep = 360f / numberOfBullet;
        float angle = 0f;
        for (int i = 0; i < numberOfBullet; i++)
        {
            float circleX = transform.position.x + Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
            float circleY = transform.position.y + Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
            Vector3 spawnPos = new Vector3(circleX, circleY, transform.position.z);

            // Tạo circle
            Instantiate(bulletPrefab, spawnPos, Quaternion.identity);

            angle += angleStep;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
