using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhaseManager : MonoBehaviour
{
    private int currentPhase = 0;
    [SerializeField] private Transform boss;
    [SerializeField] private List<BossAttackPhase> phases = new List<BossAttackPhase>();

    private void OnEnable()
    {
        EventDispatcher<bool>.AddListener(Event.BossAppear.ToString(), BossAppear);
        EventDispatcher<bool>.AddListener(Event.BossStartAttack.ToString(), StartAttack);
        EventDispatcher<bool>.AddListener(Event.BossChangePhase.ToString(), ChangePhase);
    }
    private void OnDisable()
    {
        EventDispatcher<bool>.RemoveListener(Event.BossAppear.ToString(), BossAppear);
        EventDispatcher<bool>.RemoveListener(Event.BossStartAttack.ToString(), StartAttack);
        EventDispatcher<bool>.RemoveListener(Event.BossChangePhase.ToString(), ChangePhase);
    }
    private void StartAttack(bool isAttacking)
    {
        phases[0].gameObject.SetActive(true);
    }
    private void ChangePhase(bool isChangingPhase)
    {
        if (currentPhase < phases.Count - 1)
        {
            phases[currentPhase].gameObject.SetActive(false);
            phases[currentPhase + 1].gameObject.SetActive(true);
            currentPhase++;
        }
        else return;
    }
    private void BossAppear(bool isAppear)
    {
        boss.gameObject.SetActive(isAppear);
    }    
 
}
