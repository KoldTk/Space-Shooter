using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellBombShooter : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(SpellSequence());
    }

    private IEnumerator SpellSequence()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(StartSpell());
    }

    public abstract IEnumerator StartSpell();  
}
