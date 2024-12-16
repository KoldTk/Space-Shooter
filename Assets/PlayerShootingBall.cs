using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingBall : MonoBehaviour
{
    public GameObject ballPrefab;
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
            Instantiate(ballPrefab, transform.position, transform.rotation);
            lastBulletTime = 0;
        }
        else if (lastBulletTime < shootInterval)
        {
            lastBulletTime += Time.deltaTime;
        }
    }
}
