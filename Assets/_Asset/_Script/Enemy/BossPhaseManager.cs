using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackPoint
{
    public Transform attackPoint; // Point to instantiate attack
    public Transform destination; // Destination for boss
    public List<BulletPatternBase> attackPattern;  // Attack pattern at this position
    public float attackInterval;
    public float prepareTime;
}

[System.Serializable]
public class BossPhase
{
    public string phaseName;
    public bool isSpell;
    public AttackPoint[] attackPoints;       // Attack and movement point
    public float moveSpeed = 3f;
    public float phaseTime;
}
public class BossPhaseManager : MonoBehaviour
{
    [SerializeField] private BossPhase[] phases;
    private int currentPhaseIndex = 0;
    private Coroutine phaseRoutine;

    [SerializeField] private Transform boss;
    [SerializeField] private GameObject cutInAnim;
    [SerializeField] private float attackInterval;

    private void OnEnable()
    {
        GameManager.Instance.bossInfo.phaseCount = phases.Length;
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
        StartPhase(currentPhaseIndex);
    }
    private void ChangePhase(bool isChangingPhase)
    {
        cutInAnim.SetActive(false);
        currentPhaseIndex++;
        if (currentPhaseIndex >= phases.Length)
        {
            return;
        }    

        StartPhase(currentPhaseIndex);
    }
    private void BossAppear(bool isAppear)
    {  
        boss.gameObject.SetActive(isAppear);
    }
    private void StartPhase(int index)
    {
        if (phaseRoutine != null)
            StopCoroutine(phaseRoutine);

        phaseRoutine = StartCoroutine(PhaseRoutine(phases[index]));
    }
    private IEnumerator PhaseRoutine(BossPhase phase)
    {
        int pointIndex = 0;
        yield return PreparePhase(phase);
        while (true) //Loop phase until stop
        {
            var attackPoint = phase.attackPoints[pointIndex];
            //Move to attackPoint
            yield return MoveToPoint(attackPoint.destination.position, phase.moveSpeed);
            //Prepare attack
            yield return PrepareAttack(attackPoint);
            //Attack
            yield return DoAttackAtPoint(attackPoint);
            //Go to next point
            pointIndex++;
            if (pointIndex >= phase.attackPoints.Length)
                pointIndex = 0;
        }    
    }
    private IEnumerator MoveToPoint(Vector3 target, float moveSpeed)
    {
        while ((boss.transform.position - target).sqrMagnitude > 0.01f)
        {
            boss.transform.position = Vector3.MoveTowards(boss.position, target, Time.deltaTime * moveSpeed);
            yield return null;
        }
    }
    private IEnumerator DoAttackAtPoint(AttackPoint point)
    {
        foreach (var pattern in point.attackPattern)
        {
            pattern.ExecutePattern(point.attackPoint.transform.position);
            yield return new WaitForSeconds(point.attackInterval);
        }
        yield return null;
    }
    private IEnumerator PreparePhase(BossPhase phase)
    {
        if(phase.isSpell)
        {
            cutInAnim.SetActive(true);
        }  
        yield return new WaitForSeconds(3f);
    }
    private IEnumerator PrepareAttack(AttackPoint point)
    {
        //Create prepare attack effect/shakeScreen here
        yield return new WaitForSeconds(point.prepareTime);
    }    
}
