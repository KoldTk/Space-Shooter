using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFadeIn : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private SpriteRenderer fadeOverlay;
    [SerializeField] private float fadeDuration;
    [SerializeField] private float holdDuration;
    [SerializeField] private float targetFade = 1;
    public void ShowBackground()
    {
        fadeOverlay.DOFade(1f, fadeDuration).OnComplete(() =>
        {
            sprite.enabled = true;
            DOVirtual.DelayedCall(holdDuration, () =>
            {
                fadeOverlay.DOFade(0, fadeDuration);
            });
        });
        sprite.DOFade(targetFade, 0.5f);
    }
    public void HideBackground()
    {
        sprite.DOFade(0, 0.5f);
    }    
}
