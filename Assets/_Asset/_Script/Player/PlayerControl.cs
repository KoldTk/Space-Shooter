using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Camera subCam;
    [SerializeField] private GameObject spellBomb;
    [SerializeField] private GameObject normalMode;
    [SerializeField] private GameObject focusMode;
    [SerializeField] private GameObject magicCircle;
    [SerializeField] private Collider2D playerCollider;
    private Vector3 startingPos = new Vector3(-3,-3.5f,0);
    private void OnEnable()
    {
        StartCoroutine(InvincibleSequence());
    }
    private void OnDisable()
    {
        GameManager.Instance.playerLives--;
        if (GameManager.Instance.playerLives > 0)
        {
            GameManager.Instance.playerSpell = 2;
            EventDispatcher<bool>.Dispatch(Event.CharacterDie.ToString(), true);
            StartCoroutine(SpawnSequence());
            GameManager.Instance.CharacterSpawn();
        }
        else
        {
            //Game Over or Continue
        }
    }
    void Update()
    {
        HandleCharacterMove();
        if (Input.GetKeyDown(KeyCode.X) && !GameManager.Instance.dialogueOn)
        {
            UseSpell();
        }
        SwitchModeHandler();
    } 
    private void HandleCharacterMove()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 move = new Vector3(horizontal, vertical, 0).normalized;
        transform.Translate(move * GameManager.Instance.playerSpeed * Time.deltaTime);
        transform.position = RestrictMovement();
    }
    private Vector3 RestrictMovement()
    {
        if (gameObject.CompareTag("Player_Invi")) return transform.position;
        Vector3 pos = transform.position;
        //Restrict movement
        float halfHeight = subCam.orthographicSize;
        float halfWidth = halfHeight * subCam.aspect;

        // Set sub camera as center
        Vector3 camPos = subCam.transform.position;

        float minX = camPos.x - halfWidth;
        float maxX = camPos.x + halfWidth;
        float minY = camPos.y - halfHeight;
        float maxY = camPos.y + halfHeight;

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        return pos;
    }    
    private void UseSpell()
    {
        if (!GameManager.Instance.playerUsingSpell && GameManager.Instance.playerSpell > 0)
        {
            spellBomb.SetActive(true);
            GameManager.Instance.playerSpell--;
            EventDispatcher<bool>.Dispatch(Event.UsingSpell.ToString(), true);
            GameManager.Instance.playerUsingSpell = true;
        }
    }
    private IEnumerator InvincibleSequence()
    {
        playerCollider.enabled = false;
        yield return new WaitForSeconds(5f);
        playerCollider.enabled = true;
    }
    private IEnumerator SpawnSequence()
    {
        Instantiate(magicCircle, transform.position, transform.rotation);
        yield return new WaitForSeconds (1f);
    }
    private void SwitchModeHandler()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            normalMode.SetActive(false);
            focusMode.SetActive(true);
        }
        else
        {
            normalMode.SetActive(true);
            focusMode.SetActive(false);
        }    
    }    
}
