using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGameplay : MonoBehaviour
{
    public TextMeshProUGUI bestScore;
    public TextMeshProUGUI currentScore;
    public TextMeshProUGUI powerPoint;
    public TextMeshProUGUI rewardPoint;
    private int rewardMilestone = 50;
    private void Start()
    {
        //Initial Setup
        bestScore.text = GameManager.Instance.playerBest.ToString("D12");
        currentScore.text = GameManager.Instance.playerScore.ToString("D12");
        powerPoint.text = GameManager.Instance.playerPower.ToString();
        rewardPoint.text = $"{GameManager.Instance.rewardPoint}/{rewardMilestone}";
        rewardMilestone = Mathf.Clamp(rewardMilestone, 0, 150);
    }
    private void OnEnable()
    {
        EventDispatcher<bool>.AddListener(Event.StatusChange.ToString(), UpdateStat);
        EventDispatcher<bool>.AddListener(Event.ScoreGain.ToString(), UpdateScore);
    }
    private void OnDisable()
    {
        EventDispatcher<bool>.RemoveListener(Event.StatusChange.ToString(), UpdateStat);
        EventDispatcher<bool>.RemoveListener(Event.ScoreGain.ToString(), UpdateScore);
    }
    private void UpdateStat(bool isChanged)
    {
        //Update stat on UI
        powerPoint.text = GameManager.Instance.playerPower.ToString();
        rewardPoint.text = $"{GameManager.Instance.rewardPoint}/{rewardMilestone}";
        if (GameManager.Instance.rewardPoint >= rewardMilestone)
        {
            rewardMilestone += 25;
            GameManager.Instance.rewardPoint = 0;
            rewardPoint.text = $"{GameManager.Instance.rewardPoint}/{rewardMilestone}";
        }
        if (GameManager.Instance.playerPower >= 128)
        {
            powerPoint.text = "MAX";
        }
    }
    private void UpdateScore(bool isChanged)
    {
        //Update Score on UI
        currentScore.text = GameManager.Instance.playerScore.ToString("D12");
        if (GameManager.Instance.playerScore > GameManager.Instance.playerBest)
        {
            bestScore.text = GameManager.Instance.playerScore.ToString("D12");
        }
    }    
}
