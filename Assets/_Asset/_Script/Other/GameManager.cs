using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [HideInInspector] public int playerScore;
    [HideInInspector] public int playerBest;
    [HideInInspector] public int playerLives;
    [HideInInspector] public int playerPower;
    [HideInInspector] public int playerSpell;
    [HideInInspector] public int rewardPoint;
    [HideInInspector] public int powerStage;
    [HideInInspector] public float playerSpeed;
    [HideInInspector] public int expMilestone;
    [HideInInspector] public bool playerUsingSpell;
    [HideInInspector] public bool dialogueOn;
    [HideInInspector] public BossInfo bossInfo;
    [HideInInspector] public bool allowInput = true;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform charSpawnPos;
    [SerializeField] private Transform playerParent;
    [SerializeField] private GameObject PowerUpPrefab;
    [SerializeField] private GameObject rewardPointPrefab;
    [SerializeField] private GameObject appearEffect;
    [SerializeField] private Transform gameBackground;
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
        expMilestone = 16;
        dialogueOn = false;
        bossInfo.phaseTime = Mathf.Clamp(bossInfo.phaseTime, 0, bossInfo.phaseTime);
    } 
    public void PowerUp(int value)
    {
        playerPower += value;
        EventDispatcher<bool>.Dispatch(Event.StatusChange.ToString(), true);
    }
    public void ManaPointUp(int value)
    {
        rewardPoint += value;
        EventDispatcher<bool>.Dispatch(Event.StatusChange.ToString(), true);
    }
    public void LivesUp(int value)
    {
        playerLives += value;
        EventDispatcher<bool>.Dispatch(Event.StatusChange.ToString(), true);
    }
    public void BombUp(int value)
    {
        playerSpell += value;
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
        //Enemy drop power up item when die with 60% rate
        //Enemy drop reward point when die with 40% rate
        float rate = Random.Range(0, 100f);
        if (rate < 60)
        {
            dropItem = ItemPool.Instance.GetPrefab(0, transform.position, Quaternion.identity);
        }
        else
        {
            dropItem = ItemPool.Instance.GetPrefab(1, transform.position, Quaternion.identity);
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
        Instantiate(appearEffect, charSpawnPos.position, Quaternion.identity, playerParent);
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
    public IEnumerator ScreenShake(float duration, float magnitude)
    {
        Vector3 originalPos = gameBackground.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float percentComplete = elapsed / duration;
            float damper = 1f - Mathf.Clamp01(percentComplete); // giảm dần từ 1 → 0

            float x = Random.Range(-1f, 1f) * magnitude * damper;
            float y = Random.Range(-1f, 1f) * magnitude * damper;

            gameBackground.position = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }
        gameBackground.position = originalPos;
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
