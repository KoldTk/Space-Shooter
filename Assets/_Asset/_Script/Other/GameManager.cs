using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int playerScore;
    public int playerBest;
    public int playerLives;
    public int playerPower;
    public int playerSpell;
    public int rewardPoint;
    public int powerStage;
    public float playerSpeed;
    public int expMilestone;
    public bool playerUsingSpell;
    public bool dialogueOn;
    public BossInfo bossInfo;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform charSpawnPos;
    [SerializeField] private Transform playerParent;
    [SerializeField] private GameObject PowerUpPrefab;
    [SerializeField] private GameObject rewardPointPrefab;
    private void Awake()
    {
        GameStartSetup();
    }
    private void GameStartSetup()
    {
        playerPower = Mathf.Clamp(playerPower, 0, 128);
        playerScore = 0;
        playerLives = 3;
        playerSpell = 2;
        playerPower = 1;
        rewardPoint = 0;
        powerStage = 0;
        playerSpeed = 5;
        expMilestone = 8;
        dialogueOn = false;
        bossInfo.phaseTime = Mathf.Clamp(bossInfo.phaseTime, 0, bossInfo.phaseTime);
    } 
    public void PowerUp(int value)
    {
        playerPower += value;
        EventDispatcher<bool>.Dispatch(Event.StatusChange.ToString(), true);
    }
    public void RewardPointUp(int value)
    {
        rewardPoint += value;
        EventDispatcher<bool>.Dispatch(Event.StatusChange.ToString(), true);
    }
    public void ScoreUp(int value)
    {
        playerScore += value;
        EventDispatcher<bool>.Dispatch(Event.ScoreGain.ToString(), true);
    }
    public void DropItem(Transform transform, float explodeForce)
    {
        GameObject dropItem;
        //Enemy drop power up item when die with 70% rate
        //Enemy drop reward point when die with 30% rate
        float rate = Random.Range(0, 100f);
        if (rate < 70)
        {
            dropItem = Instantiate(PowerUpPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            dropItem = Instantiate(rewardPointPrefab, transform.position, Quaternion.identity);
        }
        Rigidbody2D rb = dropItem.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            //Drop item fly to random direction when appear
            rb.AddForce(Vector2.up * explodeForce, ForceMode2D.Impulse);
        }
    }
    public void CharacterSpawn()
    {
        Instantiate(playerPrefab, charSpawnPos.position, Quaternion.identity, playerParent);  
    }
    public DialogueData LoadDialogue(string filePath)
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(filePath);
        if (jsonFile == null)
        {
            Debug.LogError($"File {filePath} does not exist.");
            return null;
        }    
        return JsonUtility.FromJson<DialogueData>(jsonFile.text);
    }  
}

public struct BossInfo
{
    public string bossName;
    public string phaseName;
    public int maxHealth;
    public float phaseTime;
    public int phaseCount;
}
