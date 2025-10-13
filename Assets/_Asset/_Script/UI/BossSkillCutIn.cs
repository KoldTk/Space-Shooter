using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossSkillCutIn : MonoBehaviour
{
    [SerializeField] private Image cutInImage;
    [SerializeField] private TextMeshProUGUI spellText;
    [SerializeField] private RectTransform imageTargetPos;
    [SerializeField] private RectTransform spellTextTarget;
    [SerializeField] private RectTransform[] warningTextLines;
    [SerializeField] private float moveDistance = 100f;
    [SerializeField] private RectTransform backgroundText;
    private Vector3[] backgroundTextStartPos;
    private Vector3 textStartingPos;
    private Vector3 imageStartingPos;
    private void Awake()
    {
        backgroundTextStartPos = new Vector3[warningTextLines.Length];
        for (int i = 0; i < warningTextLines.Length; i++)
        {
            backgroundTextStartPos[i] = warningTextLines[i].localPosition;
        }
        textStartingPos = spellText.transform.position;
        imageStartingPos = cutInImage.transform.position;
    }
    private void OnEnable()
    {
        backgroundText.gameObject.SetActive(true);
        spellText.text = GameManager.Instance.bossInfo.phaseName;
        cutInImage.transform.position = imageStartingPos;
        StartCoroutine(CutInSequence());
    }
    private void OnDisable()
    {
        spellText.transform.position = textStartingPos;
    }
    private IEnumerator CutInSequence()
    {
        ShowWarningText();
        TextFadeIn();
        ImageToFinalTarget();
        yield return new WaitForSeconds(1.5f);
        TextMoveToTarget();
        FadeInWarningText();
        yield return new WaitForSeconds(1.5f);
        HideWarningText();
        cutInImage.transform.position = imageStartingPos;
    }    
    private void TextFadeIn()
    {
        spellText.color = new Color(1, 1, 1, 0);
        spellText.transform.localScale = new Vector3(2, 2, 2);
        spellText.transform.DOScale(Vector3.one, 0.5f)
            .SetEase(Ease.InQuad);
        spellText.DOFade(1f, 0.5f);
    }
    private void TextMoveToTarget()
    {
        spellText.transform.DOMove(spellTextTarget.position, 1);
    }
    private void ImageToFinalTarget()
    {
        AnimationCurve customEase = new AnimationCurve(
        new Keyframe(0, 0, 1, 1),
        new Keyframe(0.25f, 0.5f, 0, 0),  // Slower at the middle
        new Keyframe(0.75f, 0.55f, 0, 0),  // Stay at the middle
        new Keyframe(1, 1, 1, 1)
        );
        cutInImage.transform.DOMove(imageTargetPos.position, 2f)
            .SetEase(customEase);
    }
    private void ShowWarningText()
    {
        for (int i = 0; i < warningTextLines.Length; i++)
        {
            Image sprite = warningTextLines[i].GetComponent<Image>();
            sprite.color = new Color(1f, 1f, 1f, 0);
            sprite.DOFade(0.6f, 1f);
            float direction = (i % 2 == 0) ? 1f : -1f;
            warningTextLines[i].DOLocalMoveX(direction * moveDistance, 2.5f)
                .SetRelative(true)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart);
        }
    }
    private void FadeInWarningText()
    {
        for (int i = 0; i < warningTextLines.Length; i++)
        {
            Image sprite = warningTextLines[i].GetComponent<Image>();
            sprite.DOFade(0, 1f);
        }
    }
    private void HideWarningText()
    {
        for (int i = 0; i < warningTextLines.Length; i++)
        {
            warningTextLines[i].localPosition = backgroundTextStartPos[i];
            warningTextLines[i].DOKill();
        }
    }    
}
