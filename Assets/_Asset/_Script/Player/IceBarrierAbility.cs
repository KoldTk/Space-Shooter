using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBarrierAbility : SpellBombShooter
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int numberOfBullet;
    [SerializeField] private float radius = 1f;
    [SerializeField] private float expandDuration = 1.5f;
    [SerializeField] private float holdDuration = 5f;
    [SerializeField] private float shrinkDuration = 1f;
    [SerializeField] private float rotateSpeed = 90f; // độ/giây
    private Transform circleParent;

    private void OnEnable()
    {
        StartCoroutine(StartSpell());
    }
    private void OnDisable()
    {
        GameManager.Instance.playerUsingSpell = false;
        EventDispatcher<bool>.Dispatch(Event.SpellEnd.ToString(), true);
    }
    public override IEnumerator StartSpell()
    {
        // Tạo 1 GameObject chứa vòng tròn
        circleParent = new GameObject("BulletCircle").transform;
        circleParent.position = transform.position;

        float angleStep = 360f / numberOfBullet;
        float angle = 0f;

        for (int i = 0; i < numberOfBullet; i++)
        {
            Vector3 startPos = transform.position + new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0f) * 0.1f;
            GameObject bullet = Instantiate(bulletPrefab, startPos, Quaternion.identity, circleParent);
            Vector3 targetPos = transform.position + new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0f) * radius;
            bullet.transform.DOMove(targetPos, expandDuration)
                .SetEase(Ease.OutQuad);
            angle += angleStep;
            yield return null;
        }

        circleParent.DORotate(new Vector3(0, 0, -360), 360f / rotateSpeed, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
        yield return new WaitForSeconds(expandDuration + holdDuration);
        circleParent.DOScale(0f, shrinkDuration).SetEase(Ease.InQuad)
            .OnComplete(() =>
            {
                transform.DOKill();
                circleParent.DOKill();
                Destroy(circleParent.gameObject);
                transform.gameObject.SetActive(false);
            });
    }
}
