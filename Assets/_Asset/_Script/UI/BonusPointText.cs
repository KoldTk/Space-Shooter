using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BonusPointText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI completionText;
    [SerializeField] private TextMeshProUGUI bonusPointValue;
    [SerializeField] private float destructionTime;
    private void OnEnable()
    {
        StartCoroutine(Destruction()); //launching the timer of destruction
        if (GameManager.Instance.spellSuccess)
        {
            completionText.text = "Completion Bonus";
            bonusPointValue.text = $"+{GameManager.Instance.bonusPoint}";
        }
        else
        {
            completionText.text = "Spell Failed!";
            bonusPointValue.text = "";
        }
    }

    IEnumerator Destruction() //wait for the estimated time, and destroying or deactivating the object
    {
        yield return new WaitForSeconds(destructionTime);
        gameObject.SetActive(false);
    }

}
