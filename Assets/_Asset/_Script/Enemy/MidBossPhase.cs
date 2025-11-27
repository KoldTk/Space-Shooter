using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidBossPhase : MonoBehaviour
{
    [SerializeField] private bool haveDialogue;
    [SerializeField] private string midBossDialogue;
    [SerializeField] private GameObject bossPrefab;
    private void OnEnable()
    {
        EventDispatcher<bool>.AddListener(Event.BossDie.ToString(), MidBossDie);
        if (haveDialogue)
        {
            EventDispatcher<string>.Dispatch(Event.StartDialogue.ToString(), midBossDialogue);
        }else
        {
            StartCoroutine(SpawnMidBoss());
        }    
    }
    private void OnDisable()
    {
        EventDispatcher<bool>.RemoveListener(Event.BossDie.ToString(), MidBossDie);
    }
    private IEnumerator SpawnMidBoss()
    {
        Instantiate(bossPrefab);
        yield return new WaitForSeconds(1.5f);
        EventDispatcher<bool>.Dispatch(Event.BossStartAttack.ToString(), true);
    }
    private void MidBossDie(bool isDie)
    {
        Destroy(gameObject, 2);
    }    
}
