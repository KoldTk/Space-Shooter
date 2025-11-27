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
        dialogueArea.GetComponentInChildren<DialogueManager>().OpenDialogue(filePath);
    }
}
