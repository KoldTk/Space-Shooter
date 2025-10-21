using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitVisualEffect : MonoBehaviour
{
    [SerializeField] private float liveDuration;
    [SerializeField] private int effectID;
    private SpriteRenderer sprite;
    private Vector3 moveDirection;
    [SerializeField] float moveSpeed;
    private void OnEnable()
    {
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(InitEffect());
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        moveDirection = new Vector3(randomX, randomY).normalized;
    }
    private void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
    private IEnumerator InitEffect()
    {
        sprite.DOFade(0.8f, 1);
        yield return new WaitForSeconds(liveDuration);
        sprite.DOFade(0, 1).OnComplete(() => EffectPool.Instance.ReturnToPool(effectID, gameObject));
    }
}
