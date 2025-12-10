using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialDialogue : MonoBehaviour
{
    public string beforeBossDialogue;
    public string afterBossDialogue;
    [SerializeField] private GameObject dialogueArea;
    private void Awake()
    {
        dialogueArea.SetActive(false);
    }
    private void OnEnable()
    {
        EventDispatcher<string>.AddListener(Event.StartDialogue.ToString(), InitBossDialogue);
    }
    private void OnDisable()
    {
        EventDispatcher<string>.RemoveListener(Event.StartDialogue.ToString(), InitBossDialogue);
    }
    public void InitBossDialogue(string dialoguePath)
    {

        dialogueArea.SetActive(true);
        StartDialogue(dialoguePath);
    }
    private void StartDialogue(string filePath)
    {
        ClearBullet();
        dialogueArea.GetComponentInChildren<DialogueManager>().OpenDialogue(filePath);
    }
    private void ClearBullet()
    {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bullets)
        {
            EnemyBullet enemyBullet = bullet.GetComponent<EnemyBullet>();
            if (enemyBullet != null)
            {
                enemyBullet.ChangeToPoint();
            }
        }
    }    
}
