using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStageStartIntro : MonoBehaviour
{
    [SerializeField] private Image targetImage;
    [SerializeField] private float duration = 1.5f;
    private void OnEnable()
    {
        targetImage.color = new Color(1f, 1f, 1f, 0);
        StartCoroutine(IntroSequence());
    }
    private void OnDisable()
    {
        EventDispatcher<bool>.Dispatch(Event.GameStart.ToString(), true);
    }
    private IEnumerator IntroSequence()
    {
        yield return new WaitForSeconds(3);
        FadeIn();
        yield return new WaitForSeconds(3);
        FadeOut();
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
    private void FadeIn()
    {
        targetImage.DOFade(1, duration);
    } 
    private void FadeOut()
    {
        targetImage.DOFade(0, duration);
    }    
}
