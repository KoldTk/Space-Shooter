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
        EventDispatcher<string>.AddListener(Event.StartBeforeBossDialogue.ToString(), BeforeBossDialogue);
        EventDispatcher<bool>.AddListener(Event.StartAfterBossDialogue.ToString(), AfterBossDialogue);
    }
    private void OnDisable()
    {
        EventDispatcher<string>.RemoveListener(Event.StartBeforeBossDialogue.ToString(), BeforeBossDialogue);
        EventDispatcher<bool>.RemoveListener(Event.StartAfterBossDialogue.ToString(), AfterBossDialogue);
    }
    public void BeforeBossDialogue(string dialogue)
    {
        StartCoroutine(StartDialogueSequence(5f, dialogue));
    }
    public void AfterBossDialogue(bool dialogueOn)
    {
        StartCoroutine(StartDialogueSequence(0.5f, afterBossDialogue));
    }    
    private IEnumerator StartDialogueSequence(float duration, string filePath)
    {
        dialogueArea.SetActive(true);
        yield return new WaitForSeconds(duration);
        StartDialogue(filePath);
    }
    private void StartDialogue(string filePath)
    {
        dialogueArea.GetComponentInChildren<DialogueManager>().OpenDialogue(filePath);
    }
}
