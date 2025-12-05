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
    [HideInInspector] public float evadePoint;
    [HideInInspector] public float playerSpeed;
    [HideInInspector] public int expMilestone;
    [HideInInspector] public bool playerUsingSpell;
    [HideInInspector] public bool dialogueOn;
    [HideInInspector] public BossInfo bossInfo;
    [HideInInspector] public bool allowInput = true;
    [HideInInspector] public int bonusPoint;
    [HideInInspector] public bool playerDie;
    [HideInInspector] public int retryCount;
    [HideInInspector] public Transform gameBackground;
    [HideInInspector] public bool spellSuccess;
    private int scoreMilestone;
    protected override void Awake()
    {
        base.Awake();
        GameStartSetup();
    }
    public void GameStartSetup()
    {
        playerScore = 0;
        playerLives = 3;
        playerSpell = 2;
        playerPower = 0;
        rewardPoint = 0;
        powerStage = 0;
        retryCount = 2;
        playerSpeed = 5;
        expMilestone = 16;
        scoreMilestone = 20000000;
        dialogueOn = false;
        bossInfo.phaseTime = Mathf.Clamp(bossInfo.phaseTime, 0, bossInfo.phaseTime);
    } 
    public void PowerUp(int value)
    {
        playerPower = Mathf.Clamp(playerPower, 0, 128);
        playerPower += value;
        EventDispatcher<bool>.Dispatch(Event.StatusChange.ToString(), true);
    }
    public void DeathPenalty()
    {
        playerPower = Mathf.Clamp(playerPower, 0, 128);
        playerPower /= 2;
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
        if (playerScore >= scoreMilestone)
        {
            playerLives++;
            scoreMilestone += 20000000; 
        }    
        EventDispatcher<bool>.Dispatch(Event.ScoreGain.ToString(), true);
    }
    public void DropItem(Transform transform)
    {
        GameObject dropItem;
        
        float rate = Random.Range(0, 1000f);
        if (rate < 400)
        {
            //Small power up drop rate: 40%
            if (playerPower < 128)
            {
                dropItem = ItemPool.Instance.GetPrefab(0, transform.position, Quaternion.identity);
            }
            else
            {
                //Drop energy if power max
                dropItem = ItemPool.Instance.GetPrefab(1, transform.position, Quaternion.identity);
            }    
        }
        else if (rate < 900)
        {
            //Mana drop rate: 60%
            dropItem = ItemPool.Instance.GetPrefab(1, transform.position, Quaternion.identity);
        }
        else if (rate < 945f)
        {
            //Big mana drop rate: 4.5%
            dropItem = ItemPool.Instance.GetPrefab(3, transform.position, Quaternion.identity);
        }    
        else if (rate < 999)
        {
            //Big power up drop rate: 4.5%
            dropItem = ItemPool.Instance.GetPrefab(2, transform.position, Quaternion.identity);
        }
        else
        {
            //Bonus Bomb: 1%
            dropItem = ItemPool.Instance.GetPrefab(4, transform.position, Quaternion.identity);
        }
            Rigidbody2D rb = dropItem.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            //Drop item fly to random direction when appear
            rb.AddForce(new Vector2 (Random.Range(0.2f, -0.2f), 1f) * Random.Range(1, 1.5f), ForceMode2D.Impulse);
        }
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
    public IEnumerator DeleteBullet()
    {
        GameObject[] shooters = GameObject.FindGameObjectsWithTag("Shooter");
        foreach (GameObject shooter in shooters)
        {
            Destroy(shooter);
        }
        yield return null;
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bullets)
        {
            EnemyBullet bulletObj = bullet.GetComponent<EnemyBullet>();
            if (bulletObj != null)
            {
                bulletObj.ChangeToPoint();
            }
        }
        yield return null;
    }
    public void ClearObject()
    {
        GameObject[] shooters = GameObject.FindGameObjectsWithTag("Shooter");
        foreach (GameObject shooter in shooters)
        {
            Destroy(shooter);
        }
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject item in items)
        {
            item.SetActive(false);
        }
        foreach (GameObject bullet in bullets)
        {
            bullet.SetActive(false);
        }
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
