using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceRotateShooter : MonoBehaviour
{
    [SerializeField] private float waitTime;
    [SerializeField] private float angleChange;
    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(ShootInSequence());
    }

    private IEnumerator ShootInSequence()
    {
        yield return new WaitForSeconds(0.1f);
        float currentRotation = transform.eulerAngles.z;
        while (true)
        {
            transform.DORotate(new Vector3(0, 0, currentRotation), 0);
            yield return new WaitForSeconds(waitTime);
            currentRotation += angleChange;
        }
    }    
}
