using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhaseManager : MonoBehaviour
{
    private int currentPhase = 0;
    [SerializeField] private List<BossPhase> phases = new List<BossPhase>();
    private void OnEnable()
    {
        //Listener to change phase
    }
    private void OnDisable()
    {
        //Remove Event
    }
    private void ChangePhase()
    {
        phases[currentPhase].gameObject.SetActive(false);
        phases[currentPhase + 1].gameObject.SetActive(true);
        currentPhase++;
    }    
}
