using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGameplay : MonoBehaviour
{
    [SerializeField] private GameObject introImage;
    [Header("Score UI")]
    public TextMeshProUGUI bestScore;
    public TextMeshProUGUI currentScore;

    [Header("Point UI")]
    public Slider power;
    public Slider mana;
    private int rewardMilestone = 50;

    [Header("Lives And Spell UI")]
    [SerializeField] private float spacing; //space between images
    [SerializeField] private GameObject livesImage;
    [SerializeField] private GameObject spellImage;
    [SerializeField] private Transform livesImagePosition;
    [SerializeField] private Transform spellImagePosition;
    private List<GameObject> lives = new List<GameObject>();
    private List<GameObject> spells = new List<GameObject>();

    [Header("Boss Health Bar")]
    [SerializeField] private GameObject bossHealthBar;
    private void Start()
    {
        //Initial Setup
        UpdateStat(true);
        UpdateScore(true);
        UpdateSpellLivesOnUI(true);
        rewardMilestone = Mathf.Clamp(rewardMilestone, 0, 900);
        PlayIntro();
    }
    private void OnEnable()
    {
        EventDispatcher<bool>.AddListener(Event.StatusChange.ToString(), UpdateStat);
        EventDispatcher<bool>.AddListener(Event.ScoreGain.ToString(), UpdateScore);
        EventDispatcher<bool>.AddListener(Event.CharacterDie.ToString(), UpdateSpellLivesOnUI);
        EventDispatcher<bool>.AddListener(Event.UsingSpell.ToString(), UpdateSpellLivesOnUI);
        EventDispatcher<bool>.AddListener(Event.BossStartAttack.ToString(), ShowBossHealthBar);
    }

    private void OnDisable()
    {
        EventDispatcher<bool>.RemoveListener(Event.StatusChange.ToString(), UpdateStat);
        EventDispatcher<bool>.RemoveListener(Event.ScoreGain.ToString(), UpdateScore);
        EventDispatcher<bool>.RemoveListener(Event.CharacterDie.ToString(), UpdateSpellLivesOnUI);
        EventDispatcher<bool>.RemoveListener(Event.UsingSpell.ToString(), UpdateSpellLivesOnUI);
        EventDispatcher<bool>.RemoveListener(Event.BossStartAttack.ToString(), ShowBossHealthBar);
    }

    private void UpdateStat(bool isChanged)
    {
        //Update stat on UI
        power.value = GameManager.Instance.playerPower;
        mana.value = GameManager.Instance.rewardPoint;
        mana.maxValue = rewardMilestone;
        if (GameManager.Instance.rewardPoint >= rewardMilestone)
        {
            GameManager.Instance.playerSpell++;
            rewardMilestone += 25;
            GameManager.Instance.rewardPoint = 0;
            mana.value = GameManager.Instance.rewardPoint;
            mana.maxValue = rewardMilestone;
            UpdateSpellAndLives(spells, spellImage, spellImagePosition, GameManager.Instance.playerSpell);
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
    private void UpdateSpellLivesOnUI(bool isChanged)
    {
        GameManager.Instance.playerSpell = 2;
        UpdateSpellAndLives(lives, livesImage, livesImagePosition, GameManager.Instance.playerLives - 1);
        UpdateSpellAndLives(spells, spellImage, spellImagePosition, GameManager.Instance.playerSpell);
    }    
    private void UpdateSpellAndLives(List<GameObject> list, GameObject prefab, Transform position, int numCount)
    {
        //Delete old icon when update
        foreach(GameObject obj in list)
        {
            Destroy(obj);
        }
        list.Clear();
        //Create new icon
        for (int i = 0; i < numCount; i++)
        {
            GameObject newObj = Instantiate(prefab, position);
            newObj.transform.SetParent(position, false);
            list.Add(newObj);

            //Align the image
            RectTransform rect = newObj.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(i * spacing, 0);
        }
    }
    private void PlayIntro()
    {
        introImage.SetActive(true);
    }
    private void ShowBossHealthBar(bool isOn)
    {
        bossHealthBar.SetActive(isOn);
    }

}
