using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellBombShooter : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(StartSpell());
    }
    private void OnDisable()
    {
        GameManager.Instance.playerUsingSpell = false;
        EventDispatcher<bool>.Dispatch(Event.SpellEnd.ToString(), true);
    }
    public abstract IEnumerator StartSpell();  
}
