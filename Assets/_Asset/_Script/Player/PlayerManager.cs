using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject magicCircle;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform charSpawnPos;
    private bool isDead;
    private void Awake()
    {
        CharacterSpawn();
    }
    private void OnEnable()
    {
        EventDispatcher<bool>.AddListener(Event.CharacterDie.ToString(), RespawnCharacter);
        EventDispatcher<bool>.AddListener(Event.Retry.ToString(), Retry);
    }
    private void OnDisable()
    {
        EventDispatcher<bool>.RemoveListener(Event.CharacterDie.ToString(), RespawnCharacter);
        EventDispatcher<bool>.RemoveListener(Event.Retry.ToString(), Retry);
    }
    void Start()
    {
        GameManager.Instance.gameBackground = GameObject.FindGameObjectWithTag("Background").transform;
    }
    private void RespawnCharacter(bool isSpawn)
    {
        GameManager.Instance.playerLives--;
        if (GameManager.Instance.playerLives > 0)
        {
            GameManager.Instance.playerSpell = 2;

            for (int i = 0; i < 10; i++)
            {
                GameManager.Instance.DropItem(transform);
            }
            StartCoroutine(DeleteBullet());
            CharacterSpawn();
        }
        else
        {
            if (GameManager.Instance.retryCount > 0)
            {
                EventDispatcher<bool>.Dispatch(Event.OpenRetryMenu.ToString(), true);
            }
            else
            {
                SceneManager.LoadScene("Result");
            }
        }
    }
    private IEnumerator DeleteBullet()
    {
        Instantiate(magicCircle, transform.position, transform.rotation);
        yield return new WaitForSeconds(1f);
    }
    public void CharacterSpawn()
    {
        Instantiate(playerPrefab, charSpawnPos.position, Quaternion.identity, transform);
    }
    public void Retry(bool retry)
    {
        GameManager.Instance.retryCount--;
        GameManager.Instance.LivesUp(3);
        DeleteBullet();
        CharacterSpawn();
    }    
}
