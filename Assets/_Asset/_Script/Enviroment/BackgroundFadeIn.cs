using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFadeIn : MonoBehaviour
{
    private SpriteRenderer background;
    [SerializeField] private SpriteRenderer fadeOverlay;
    [SerializeField] private float fadeDuration;
    [SerializeField] private float holdDuration;
    [SerializeField] private float targetFade = 1;
    private void Start()
    {
        background = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        EventDispatcher<Sprite>.AddListener(Event.ShowSpellBackground.ToString(), ShowBackground);
        EventDispatcher<Sprite>.AddListener(Event.HideSpellBackground.ToString(), HideBackground);
    }
    private void OnDisable()
    {
        EventDispatcher<Sprite>.RemoveListener(Event.ShowSpellBackground.ToString(), ShowBackground);
        EventDispatcher<Sprite>.RemoveListener(Event.HideSpellBackground.ToString(), HideBackground);
    }
    public void ShowBackground(Sprite targetSprite)
    {
        background.sprite = targetSprite;
        fadeOverlay.DOFade(1f, fadeDuration).OnComplete(() =>
        {
            background.enabled = true;
            DOVirtual.DelayedCall(holdDuration, () =>
            {
                fadeOverlay.DOFade(0, fadeDuration);
            });
        });
        background.DOFade(targetFade, 0.5f);
    }
    public void HideBackground(Sprite targetSprite)
    {
        background.sprite = targetSprite;
        background.DOFade(0, 0.5f);
    }    
}
