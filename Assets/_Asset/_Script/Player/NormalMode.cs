using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMode : MonoBehaviour
{
    [SerializeField] private float normalModeSpeed;
    private List<Transform> guns = new List<Transform>();
    private void Awake()
    {
        foreach (Transform child in transform)
        {
            guns.Add(child);
            child.gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        GameManager.Instance.playerSpeed = normalModeSpeed;
        EventDispatcher<bool>.AddListener(Event.StatusChange.ToString(), LevelUp);
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].gameObject.SetActive(false);
        }
        guns[GameManager.Instance.powerStage].gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        EventDispatcher<bool>.RemoveListener(Event.StatusChange.ToString(), LevelUp);
    }
    private void LevelUp(bool isChanged)
    {
        //Power up if gain enough power point
        if (GameManager.Instance.playerPower >= GameManager.Instance.expMilestone && GameManager.Instance.powerStage < 5)
        {
            GameManager.Instance.expMilestone *= 2;
            GameManager.Instance.powerStage++;
            ChangeGunStage();
        }
    }
    private void ChangeGunStage()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].gameObject.SetActive(false);
        }
        guns[GameManager.Instance.powerStage].gameObject.SetActive(true);
    }
}
