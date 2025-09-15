using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDialogueButton : MonoBehaviour
{
    public string filePath;
    public void StartDialogue()
    {
        FindObjectOfType<DialogueManager>().OpenDialogue(filePath);
    }
}
