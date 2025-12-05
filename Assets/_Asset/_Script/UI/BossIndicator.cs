using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BossIndicator : MonoBehaviour
{
    private GameObject boss;
    [SerializeField] private RectTransform indicatorUI;
    [SerializeField] private RectTransform indicator;
    [SerializeField] private float minX = 10;
    [SerializeField] private float maxX = 10f;
    private float barWidth;
    private void Start()
    {
        barWidth = indicatorUI.rect.width;
    }
    private void OnEnable()
    {
        EventDispatcher<bool>.AddListener(Event.BossAppear.ToString(), ShowIndicator);
        EventDispatcher<bool>.AddListener(Event.BossDie.ToString(), HideIndicator);
    }
    private void OnDisable()
    {
        EventDispatcher<bool>.RemoveListener(Event.BossAppear.ToString(), ShowIndicator);
        EventDispatcher<bool>.RemoveListener(Event.BossDie.ToString(), HideIndicator);
    }

    void Update()
    {
        if (boss == null) return;
        float bossX = boss.transform.position.x;
        float t = Mathf.InverseLerp(minX, maxX, bossX);
        float uiX = Mathf.Lerp(-barWidth / 2f, barWidth / 2f, t);
        Vector2 pos = indicator.anchoredPosition;

        pos.x = uiX;
        indicator.anchoredPosition = pos;
    }
    private void ShowIndicator(bool isOn)
    {
        indicatorUI.gameObject.SetActive(true);
        if (boss == null)
        {
            boss = GameObject.FindGameObjectWithTag("Boss");
        }    
    }
    private void HideIndicator(bool isOn)
    {
        indicatorUI.gameObject.SetActive(false);
        boss = null;
    }    
}
