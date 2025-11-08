using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusMode : MonoBehaviour
{
    [SerializeField] private float focusModeSpeed;
    [SerializeField] private GameObject marker;
    [SerializeField] private GameObject magicCircle;
    private List<Transform> guns = new List<Transform>();
    private void Awake()
    {
        SetupGun();
    }
    private void OnEnable()
    {
        ShowMarker();
        GameManager.Instance.playerSpeed = focusModeSpeed;
        EventDispatcher<bool>.AddListener(Event.StatusChange.ToString(), LevelUp);
        EventDispatcher<bool>.AddListener(Event.CharacterDie.ToString(), ReducePower);
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].gameObject.SetActive(false);
        }
        guns[GameManager.Instance.powerStage].gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        HideMarker();
        EventDispatcher<bool>.RemoveListener(Event.StatusChange.ToString(), LevelUp);
        EventDispatcher<bool>.RemoveListener(Event.CharacterDie.ToString(), ReducePower);
    }
    private void LevelUp(bool isChanged)
    {
        //Power up if gain enough power point
        if (GameManager.Instance.playerPower >= GameManager.Instance.expMilestone && GameManager.Instance.powerStage < 5)
        {
            GameManager.Instance.expMilestone *= 2;
            GameManager.Instance.powerStage++;;
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
    private void ReducePower(bool isChanged)
    {
        if (GameManager.Instance.expMilestone > 16)
        {
            GameManager.Instance.expMilestone /= 2;
        }    
        GameManager.Instance.playerPower /= 2;
        if (GameManager.Instance.powerStage > 0)
        {
            GameManager.Instance.powerStage--;
        }
        GameManager.Instance.DeathPenalty();
    }
    private void ShowMarker()
    {
        marker.gameObject.SetActive(true);
        magicCircle.gameObject.SetActive(true);
    }
    private void HideMarker()
    {
        marker.gameObject.SetActive(false);
        magicCircle.gameObject.SetActive(false);
    }
    private void SetupGun()
    {
        foreach (Transform child in transform)
        {
            guns.Add(child);
            child.gameObject.SetActive(false);
        }
    }    
}
