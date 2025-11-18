using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidBossPhaseManager : MonoBehaviour
{
    [SerializeField] private BossPhase[] phases;
    private int currentPhaseIndex = 0;
    private Coroutine phaseRoutine;

    [SerializeField] private Transform boss;
    [SerializeField] private Transform startPos;
    [SerializeField] private GameObject cutInAnim;
    [SerializeField] private string bossName;
    [SerializeField] private Sprite spellSprite;
    [SerializeField] private GameObject magicCircle;
    private void OnEnable()
    {   
        GameManager.Instance.bossInfo.phaseCount = phases.Length;
        GameManager.Instance.bossInfo.bossName = bossName;
        EventDispatcher<bool>.AddListener(Event.MidBossAppear.ToString(), BossAppear);
        EventDispatcher<bool>.AddListener(Event.BossChangePhase.ToString(), ChangePhase);
        EventDispatcher<bool>.AddListener(Event.SpellEnd.ToString(), HideSpellBackground);
        EventDispatcher<bool>.AddListener(Event.BossDie.ToString(), StopAction);
    }

    private void OnDisable()
    {
        EventDispatcher<bool>.RemoveListener(Event.BossChangePhase.ToString(), ChangePhase);
        EventDispatcher<bool>.RemoveListener(Event.BossDie.ToString(), StopAction);
        EventDispatcher<bool>.RemoveListener(Event.SpellEnd.ToString(), HideSpellBackground);
    }
    private void StartAttack(bool isAttacking)
    {
        StartPhase(currentPhaseIndex);
    }
    private void ChangePhase(bool isChangingPhase)
    {
        GameManager.Instance.StartCoroutine(GameManager.Instance.DeleteBullet());
        boss.tag = "Boss_Invi";
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
        boss.tag = "Boss_Invi";
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
        GameManager.Instance.bossInfo.phaseTime = phase.phaseTime;
        GameManager.Instance.bossInfo.phaseName = phase.phaseName;
        EventDispatcher<int>.Dispatch(Event.UpdateBossHP.ToString(), phase.phaseHP);
        int pointIndex = 0;
        yield return PreparePhase(phase);
        boss.tag = "Boss";
        while (true) //Loop phase until stop
        {
            var attackPoint = phase.attackPoints[pointIndex];
            //Move to attackPoint
            yield return MoveToPoint(attackPoint.destination.position, phase.moveDuration);
            //Prepare attack
            yield return PrepareAttack(attackPoint);
            //Attack
            yield return DoAttackAtPoint(attackPoint);
            //Go to next point
            pointIndex++;
            if (pointIndex >= phase.attackPoints.Length)
            {
                pointIndex = 0;
                yield return MoveToPoint(startPos.position, phase.moveDuration);
                yield return new WaitForSeconds(phase.loopDelay);
            }
        }
    }
    private IEnumerator MoveToPoint(Vector3 target, float moveDuration)
    {
        if (boss != null)
        {
            Tween tween = boss.transform.DOMove(target, moveDuration)
            .SetEase(Ease.OutQuad);
            yield return tween.WaitForCompletion();
        }
    }
    private IEnumerator DoAttackAtPoint(AttackPoint point)
    {
        GameObject shooter = Instantiate(point.shooterPrefab, point.destination.position, Quaternion.identity);
        if (point.shootWhileMoving)
        {
            shooter.transform.SetParent(boss);
        }
        Destroy(shooter, point.shooterDuration);
        //Wait before new action
        yield return new WaitForSeconds(point.actionDelay);
    }
    private IEnumerator PreparePhase(BossPhase phase)
    {
        yield return MoveToPoint(startPos.position, 1);
        if (phase.isSpell)
        {
            cutInAnim.SetActive(true);
            magicCircle.SetActive(true);
            EventDispatcher<Sprite>.Dispatch(Event.ShowSpellBackground.ToString(), spellSprite);
        }
        yield return new WaitForSeconds(1f);
    }
    private IEnumerator PrepareAttack(AttackPoint point)
    {
        //Create prepare attack effect/shakeScreen here
        yield return new WaitForSeconds(point.prepareTime);
    }
    private void HideSpellBackground(bool isHided)
    {
        magicCircle.SetActive(false);
        EventDispatcher<Sprite>.Dispatch(Event.HideSpellBackground.ToString(), spellSprite);
    }
    private void StopAction(bool isDead)
    {
        StopAllCoroutines();
    }
}
