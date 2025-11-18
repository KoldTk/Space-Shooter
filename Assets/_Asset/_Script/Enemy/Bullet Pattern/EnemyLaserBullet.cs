using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserBullet : MonoBehaviour
{
    [SerializeField] private float liveDuration;
    [SerializeField] private SpriteRenderer sprite;
    private float currentTime;
    private Tween tween;
    private IEnumerator Start()
    {
        DOTween.SetTweensCapacity(500, 50);
        yield return new WaitForSeconds(Random.Range(0f, 0.2f));
        sprite.DOFade(0.9f, 1);
        tween = transform.DOScale(new Vector3(1, 1, 0), 0.2f);
        currentTime = liveDuration;
    }
    private void OnDisable()
    {
        transform.DOKill();
    }
    private void OnDestroy()
    {
        transform.DOKill();
    }
    void Update()
    {
        if (liveDuration > 0)
        {
            liveDuration -= Time.deltaTime;
        }
        else
        {
            StartCoroutine(FadeOutLaser());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(1);
            }
        }
    }
    private IEnumerator FadeOutLaser()
    {
        yield return new WaitForSeconds(Random.Range(0f, 0.2f));
        transform.DOScale(new Vector3(1, 0, 0), 1)
        .OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }    
}
