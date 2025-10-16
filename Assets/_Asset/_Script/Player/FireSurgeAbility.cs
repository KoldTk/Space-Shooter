using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSurgeAbility : MonoBehaviour
{
    [SerializeField] private GameObject fireSurgeBullet;
    [SerializeField] private float shakeDuration;
    [SerializeField] private float shakeMagnitude;
    private FireSurgeBullet bullet;
    private void OnEnable()
    {
        StartCoroutine(GameManager.Instance.ScreenShake(shakeDuration, shakeMagnitude));
        bullet = Instantiate(fireSurgeBullet, transform.position, transform.rotation).GetComponentInChildren<FireSurgeBullet>(); ;
    }
    private void Update()
    {
        if (bullet != null)
        {
            bullet.transform.position = transform.position;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
