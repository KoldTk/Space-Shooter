using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingGun : MonoBehaviour
{
    public GameObject gunPrefab;
    public float shootInterval;
    private float lastBulletTime;
    // Start is called before the first frame update
    void Start()
    {
        lastBulletTime = shootInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (lastBulletTime >= shootInterval)
        {
            Shooting();
            lastBulletTime = 0;
        }
        else if (lastBulletTime < shootInterval)
        {
            lastBulletTime += Time.deltaTime;
        }
    }

    void Shooting()
    {
        if (Input.GetMouseButton(0))
        {
            Instantiate(gunPrefab, transform.position, transform.rotation);
        }
    }
}
