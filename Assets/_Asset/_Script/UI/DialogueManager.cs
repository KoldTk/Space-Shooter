using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private Image leftActorImage;
    [SerializeField] private TextMeshProUGUI leftActorName;
    [SerializeField] private Image rightActorImage;  // Sub Slot
    [SerializeField] private TextMeshProUGUI rightActorName;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color dimColor = Color.gray;
    public TextMeshProUGUI message;
    private DialogueData currentDialogue;
    private int leftActorId = 0;
    private int rightActorId = -1;
    private int lastSpeakingActor = -1; //Last people talk
    private int activeMessage = 0;
    private float textSpeed;
    private bool bossBattleStart = true;
    private Dictionary<int, string> actorPositions = new Dictionary<int, string>();
    private void OnEnable()
    {
        //Load text speed from playerPref here
        textSpeed = 0.1f;
        transform.localScale = Vector3.zero;
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X)) && GameManager.Instance.dialogueOn)
        {
            if (message.text == currentDialogue.messages[activeMessage].message)
            {
                NextMessage();
            }
            else
            {
                StopAllCoroutines();
                message.text = currentDialogue.messages[activeMessage].message;
            }
        }
    }
    public void OpenDialogue(string fileName)
    {
        currentDialogue = GameManager.Instance.LoadDialogue(fileName);
        if (currentDialogue == null) return;

        activeMessage = 0;
        GameManager.Instance.dialogueOn = true;
        transform.DOScale(Vector3.one, 0.5f);
        DisplayMessage();
    }    
    public void DisplayMessage()
    {
        MessageData messageToDisplay = currentDialogue.messages[activeMessage];
        message.text = string.Empty;
        StartCoroutine(TypeMessageLine(messageToDisplay.message));
        ActorData actorToDisplay = currentDialogue.actors[messageToDisplay.actorId];
        DisplayCharacterImage(actorToDisplay);
    }
    private IEnumerator CloseMessageBox()
    {
        transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InOutExpo);
        GameManager.Instance.dialogueOn = false;
        yield return new WaitForSeconds(0.6f);
        if (bossBattleStart)
        {
            EventDispatcher<bool>.Dispatch(Event.BossStartAttack.ToString(), true);
            bossBattleStart = false;
        }
        else
        {
            //To result menu
        }    
    }    
    private IEnumerator TypeMessageLine(string dialogueLine)
    {
        foreach (char character in dialogueLine)
        {
            message.text += character;
            yield return new WaitForSeconds(textSpeed);
        }
    }    
    public void NextMessage()
    {
        activeMessage++;
        if (activeMessage < currentDialogue.messages.Length)
        {
            DisplayMessage();
        }
        else
        {
            StartCoroutine(CloseMessageBox());
        }    
    }
    private void DisplayCharacterImage(ActorData actorToDisplay)
    {
        int speakerId = actorToDisplay.id;
        if (!leftActorImage.isActiveAndEnabled)
        {
            leftActorImage.gameObject.SetActive(true);
        }
        if (speakerId == 0)
        {
            //Main character always at left side
            leftActorImage.sprite = Resources.Load<Sprite>(actorToDisplay.spritePath);
            leftActorName.text = actorToDisplay.name;
            leftActorImage.color = normalColor;
            rightActorImage.color = dimColor;
            actorPositions[speakerId] = "left";
        }
        else
        {
            if (!rightActorImage.isActiveAndEnabled)
            {
                rightActorImage.gameObject.SetActive(true);
            }
            if (!actorPositions.ContainsKey(speakerId))
            {
                //New actor appear -> Go to emtpy slot
                //Main character -> To left slot
                if (leftActorId == 0) // Main character on left
                {
                    rightActorId = speakerId;
                    rightActorImage.sprite = Resources.Load<Sprite>(actorToDisplay.spritePath);
                    rightActorName.text = actorToDisplay.name;
                    actorPositions[speakerId] = "right";
                }
                else
                {
                    // If no empty slot -> Switch left image (except main character)
                    leftActorId = speakerId;
                    leftActorImage.sprite = Resources.Load<Sprite>(actorToDisplay.spritePath);
                    leftActorName.text = actorToDisplay.name;
                    actorPositions[speakerId] = "left";
                }
                EventDispatcher<bool>.Dispatch(Event.BossAppear.ToString(), true);
            }
            else
            {
                if (leftActorId == 0)
                {
                    rightActorId = speakerId;
                    rightActorImage.sprite = Resources.Load<Sprite>(actorToDisplay.spritePath);
                    rightActorName.text = actorToDisplay.name;
                    actorPositions[speakerId] = "right";
                }
                else
                {
                    // If no empty slot -> Switch left image (except main character)
                    leftActorId = speakerId;
                    leftActorImage.sprite = Resources.Load<Sprite>(actorToDisplay.spritePath);
                    leftActorName.text = actorToDisplay.name;
                    actorPositions[speakerId] = "left";
                }
            }
            // Highlight speaking actor
            if (actorPositions[speakerId] == "left")
            {
                leftActorImage.color = normalColor;
                rightActorImage.color = dimColor;
            }
            else
            {
                rightActorImage.color = normalColor;
                leftActorImage.color = dimColor;
            }
        } 
        lastSpeakingActor = speakerId;
    }
}
[System.Serializable]
public class DialogueSlot
{
    public int actorId = -1;
    public Image avatarImage;
    public TextMeshProUGUI actorName;
}
