using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MagicCircleMovement : MonoBehaviour
{
    [SerializeField] private Vector3 targetScale = new Vector3(2f, 2f, 2f);
    [SerializeField] private float duration = 1f;
    private void OnEnable()
    {
        CircleRotate();
        StartCoroutine(Enlarge(targetScale, duration));
    }
    private void OnDisable()
    {
        GameManager.Instance.playerUsingSpell = false;
        transform.DOKill();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
        }    
    }
    private void CircleRotate()
    {
        transform.DORotate(new Vector3(0, 0, -360), 360f / 90f, RotateMode.FastBeyond360)
         .SetEase(Ease.Linear)
         .SetLoops(-1, LoopType.Restart);
    }
    private IEnumerator Enlarge(Vector3 target, float time)
    {
        Vector3 initialScale = transform.localScale;
        Vector3 shrinkScale = new Vector3(0.1f, 0.1f, 0);
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            transform.localScale = Vector3.Lerp(initialScale, target, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        //Set target scale
        transform.localScale = target;
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
    } 
}
