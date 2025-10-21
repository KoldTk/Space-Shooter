using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BonusPointText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bonusPointValue;
    [SerializeField] private float destructionTime;
    private void OnEnable()
    {
        StartCoroutine(Destruction()); //launching the timer of destruction
        bonusPointValue.text = $"+{GameManager.Instance.bonusPoint}";
    }

    IEnumerator Destruction() //wait for the estimated time, and destroying or deactivating the object
    {
        yield return new WaitForSeconds(destructionTime);
        gameObject.SetActive(false);
    }

}
