using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject magicCircle;
    [SerializeField] private Transform charSpawnPos;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private SpriteRenderer fadeOverlay;
    [SerializeField] private float fadeDuration;
    [SerializeField] private float holdDuration;
    [SerializeField] private float targetFade = 1;
    [SerializeField] private GameObject fullPowerItem;
    [SerializeField] private Transform dropItemLocation;
    private void OnEnable()
    {
        EventDispatcher<bool>.AddListener(Event.CharacterDie.ToString(), RespawnCharacter);
        EventDispatcher<bool>.AddListener(Event.Retry.ToString(), Retry);
        EventDispatcher<bool>.AddListener(Event.PlayerUsingSpell.ToString(), ShowBackground);
        EventDispatcher<bool>.AddListener(Event.PlayerSpellEnd.ToString(), HideBackground);
    }
    private void OnDisable()
    {
        EventDispatcher<bool>.RemoveListener(Event.CharacterDie.ToString(), RespawnCharacter);
        EventDispatcher<bool>.RemoveListener(Event.Retry.ToString(), Retry);
        EventDispatcher<bool>.RemoveListener(Event.PlayerUsingSpell.ToString(), ShowBackground);
        EventDispatcher<bool>.RemoveListener(Event.PlayerSpellEnd.ToString(), HideBackground);
    }
    void Start()
    {
        CharacterSpawn();
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
                for (int i = 0; i < 10; i++)
                {
                    GameObject dropItem = Instantiate(fullPowerItem, dropItemLocation.position, Quaternion.identity);
                    Rigidbody2D rb = dropItem.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        //Drop item fly to random direction when appear
                        rb.AddForce(new Vector2(Random.Range(-0.5f, 0.5f), 2), ForceMode2D.Impulse);
                    }
                }
            }
            else
            {
                GameManager.Instance.ClearObject();
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
        GameObject playerPrefab = GameManager.Instance.selectedCharacter;
        Instantiate(playerPrefab, charSpawnPos.position, Quaternion.identity, transform);
        GameManager.Instance.isChangingScene = false;
    }
    public void Retry(bool retry)
    {
        GameManager.Instance.retryCount--;
        GameManager.Instance.LivesUp(3);
        DeleteBullet();
        CharacterSpawn();
    }
    private void ShowBackground(bool isUsingSpell)
    {
        fadeOverlay.DOFade(1f, fadeDuration).OnComplete(() =>
        {
            sprite.enabled = true;
            DOVirtual.DelayedCall(holdDuration, () =>
            {
                fadeOverlay.DOFade(0, fadeDuration);
            });
        });
        sprite.DOFade(targetFade, 0.5f);
    }
    private void HideBackground(bool spellEnd)
    {
        sprite.DOFade(0, 0.5f);
    }  
}
