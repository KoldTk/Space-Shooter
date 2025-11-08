using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI stageClearBonus;
    [SerializeField] private TextMeshProUGUI noDeathBonus;
    [SerializeField] private TextMeshProUGUI grazeBonus;
    [SerializeField] private TextMeshProUGUI totalBonus;
    [SerializeField] private int stageClearPoint;
    [SerializeField] private int noDeathPoint;
    [SerializeField] private GameObject[] texts;
    [SerializeField] private GameObject changeSceneButton;
    [SerializeField] private bool isEndStage = false;
    private void OnEnable()
    {
        if (!isEndStage)
        {
            transform.DOScale(0, 1).OnComplete(() =>
            { 
                isEndStage = true; 
                gameObject.SetActive(false);
            });
        }
        else
        {
            if (GameManager.Instance.playerDie)
            {
                noDeathPoint = 0; 
                GameManager.Instance.playerDie = false;
            }    
            StartCoroutine(ShowResult());
        }
    }
    private IEnumerator ShowResult()
    {
        int grazePoint = (int)(GameManager.Instance.evadePoint * 500);
        int totalPoint = stageClearPoint + noDeathPoint + grazePoint;
        transform.DOScale(1, 1);
        stageClearBonus.text = stageClearPoint.ToString();
        noDeathBonus.text = noDeathPoint.ToString();
        grazeBonus.text = grazePoint.ToString();
        totalBonus.text = totalPoint.ToString();
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject text in texts)
        {
            text.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
        GameManager.Instance.ScoreUp(totalPoint);
        changeSceneButton.SetActive(true);
    }   
}
