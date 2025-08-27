using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBulletShooter : MonoBehaviour
{
    public GameObject ballPrefab;
    public float shootInterval;
    private float lastBulletTime;
    [SerializeField] private int bulletID;
    // Start is called before the first frame update
    void Start()
    {
        lastBulletTime = shootInterval;
    }
    void Update()
    {
        Shooting();
    }
    void Shooting()
    {
        if (Input.GetMouseButton(0))
        {
            if (lastBulletTime >= shootInterval)
            {
                BulletPool.Instance.GetPrefab(bulletID, transform.position, transform.rotation);
                lastBulletTime = 0;
            }
            else if (lastBulletTime < shootInterval)
            {
                lastBulletTime += Time.deltaTime;
            }
        }
    }

}
