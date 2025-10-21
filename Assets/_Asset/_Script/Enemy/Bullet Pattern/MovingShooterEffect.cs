using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingShooterEffect : MonoBehaviour
{
    [SerializeField] private FlyPath flyPath;
    [SerializeField] private float flySpeed;
    [SerializeField] private int effectID;
    [SerializeField] private float effectInterval;
    [SerializeField] private GameObject shooter;
    private float interval;
    private int nextIndex;
    // Start is called before the first frame update
    private void Start()
    {
        interval = effectInterval;
    }

    // Update is called once per frame
    void Update()
    {

        if (flyPath == null)
        {
            return;
        }
        if (nextIndex >= flyPath.wayPoints.Length)
        {
            return;
        }
        if (transform.position != flyPath[nextIndex])
        {
            FlyToNextWaypoint();
            CreateEffect();
        }
        else
        {
            nextIndex++;
        }
    }
    private void FlyToNextWaypoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, flyPath[nextIndex], flySpeed * Time.deltaTime);
        if (transform.position == flyPath[nextIndex])
        {
            shooter.SetActive(true);
        }
        else
        {
            shooter.SetActive(false);
        }
    }

    private void CreateEffect()
    {
        if (interval > 0)
        {
            interval -= Time.deltaTime;
        }
        else
        {
            EffectPool.Instance.GetPrefab(effectID, transform.position, transform.rotation);
            interval = effectInterval;
        }
    }
}
