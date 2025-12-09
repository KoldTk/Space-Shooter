using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class MenuButton : MonoBehaviour
{
    private TextMeshProUGUI text;
    private RectTransform rect;
    private Vector3 startPosition;
    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        rect = text.GetComponent<RectTransform>();
        startPosition = text.rectTransform.localPosition;
    }
    private void OnEnable()
    {
       MoveToPosition();
    }
    private void OnDisable()
    {
        rect.localPosition = startPosition;
    }
    private void MoveToPosition()
    {
        rect.DOAnchorPos(new Vector3(0, 0, 0), 0.5f);
        FadeIn();
    }
    private void FadeIn()
    {
        text.DOFade(0.1f, 0).OnComplete(() => { text.DOFade(1, 0.5f); });
    }    
    public abstract void ClickEvent();
}
