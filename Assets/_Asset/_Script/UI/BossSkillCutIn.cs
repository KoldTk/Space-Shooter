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
    [SerializeField] private RectTransform imageMidTarget;
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
        ImageMoveToTarget();
        yield return new WaitForSeconds(2f);
        ImageReturn();
        HideWarningText();
        yield return new WaitForSeconds(0.5f);
        TextMoveToTarget();
        yield return new WaitForSeconds(5);
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
    private void ImageMoveToTarget()
    {
        cutInImage.transform.DOMove(imageMidTarget.position, 1)
            .SetEase(Ease.OutQuad);
    }
    private void ImageReturn()
    {
        cutInImage.transform.DOMove(imageStartingPos, 0.5f)
            .SetEase(Ease.Linear);
    }
    private void ShowWarningText()
    {
        for (int i = 0; i < warningTextLines.Length; i++)
        {
            float direction = (i % 2 == 0) ? 1f : -1f;
            warningTextLines[i].DOLocalMoveX(direction * moveDistance, 2.5f)
                .SetRelative(true)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart);
        }
    }
    private void HideWarningText()
    {
        backgroundText.gameObject.SetActive(false);
        for (int i = 0; i < warningTextLines.Length; i++)
        {
            warningTextLines[i].DOKill();
            warningTextLines[i].localPosition = backgroundTextStartPos[i];
        } 
    }    
}
