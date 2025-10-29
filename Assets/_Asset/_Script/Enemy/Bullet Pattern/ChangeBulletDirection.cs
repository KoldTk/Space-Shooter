using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBulletDirection : MonoBehaviour
{
    private Vector3 moveDirection;
    [SerializeField] float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShakeScreen());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
    private IEnumerator ShakeScreen()
    {
        GameManager.Instance.ScreenShake(1, 1);
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bullets)
        {
            float randomX = Random.Range(-1f, 1f);
            float randomY = Random.Range(-1f, 1f);
            moveDirection = new Vector3(randomX, randomY).normalized;
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null )
            {
                rb.velocity = moveDirection * moveSpeed;
            }    
            yield return null;
        }
    }
}
