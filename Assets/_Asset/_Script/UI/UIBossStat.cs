using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class UIBossStat : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bossName;
    [SerializeField] private Slider bossHealthBar;
    [SerializeField] private TextMeshProUGUI timeCounter;
    [SerializeField] private GameObject phaseIcon;
    [SerializeField] private Transform phaseIconTransform;
    [SerializeField] private float spacing;
    [SerializeField] private float healthFillSpeed;
    private float time;
    private int phaseCount;
    private int maxHealth;
    private int currentHealth;
    private List<GameObject> phases = new List<GameObject>();

    private void Start()
    {
        bossName.text = GameManager.Instance.bossInfo.name;
        phaseCount = GameManager.Instance.bossInfo.phaseCount;
        time = GameManager.Instance.bossInfo.timeCounter;
        timeCounter.text = ((int)time).ToString();
        UpdatePhaseIcon(phases, phaseIcon, phaseIconTransform, phaseCount);
        ShowHealthBar(GameManager.Instance.bossInfo.maxHealth);
    }
    private void OnEnable()
    {
        EventDispatcher<int>.AddListener(Event.BossTakeDamage.ToString(), UpdateBossHealth);
        EventDispatcher<bool>.AddListener(Event.BossChangePhase.ToString(), RefillHealth);
    }
    private void OnDisable()
    {
        EventDispatcher<int>.RemoveListener(Event.BossTakeDamage.ToString(), UpdateBossHealth);
        EventDispatcher<bool>.RemoveListener(Event.BossChangePhase.ToString(), RefillHealth);
    }
    private void UpdateBossHealth(int dmg)
    {
        currentHealth = Mathf.Max(0, currentHealth - dmg);
        bossHealthBar.DOKill();
        bossHealthBar.DOValue(currentHealth, 0.1f)
            .SetEase(Ease.Linear);
    }
    private void UpdatePhaseIcon(List<GameObject> list, GameObject prefab, Transform position, int numCount)
    {
        //Delete old icon when update
        foreach (GameObject obj in list)
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
    private void RefillHealth(bool isNewPhase)
    {
        phaseCount--;
        maxHealth = GameManager.Instance.bossInfo.maxHealth;
        currentHealth = maxHealth;

        UpdatePhaseIcon(phases, phaseIcon, phaseIconTransform, phaseCount);
        bossHealthBar.DOValue(currentHealth, 1)
        .SetEase(Ease.Linear);
    }    
    private void ShowHealthBar(int targetHealth)
    {
        maxHealth = targetHealth;
        currentHealth = targetHealth;

        bossHealthBar.maxValue = maxHealth;
        bossHealthBar.value = 0;
        bossHealthBar.DOValue(targetHealth, 1)
            .SetEase(Ease.Linear);
    }
  
}
