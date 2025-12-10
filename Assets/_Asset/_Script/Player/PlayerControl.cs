using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Camera subCam;
    [SerializeField] private GameObject spellBomb;
    [SerializeField] private GameObject normalMode;
    [SerializeField] private GameObject focusMode;
    [SerializeField] private Collider2D playerCollider;

    private void OnEnable()
    {
        StartCoroutine(InvincibleSequence());
    }
    private void OnDisable()
    {
        if (!GameManager.Instance.isChangingScene)
        {
            EventDispatcher<bool>.Dispatch(Event.CharacterDie.ToString(), true);
        }    
        EventDispatcher<bool>.Dispatch(Event.PlayerSpellEnd.ToString(), true);
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
        Vector3 pos = transform.position;

        float halfHeight = subCam.orthographicSize;
        float aspect = (float)Screen.width / (float)Screen.height;
        float halfWidth = halfHeight * aspect;

        // If camera only take a path of the screen
        halfWidth *= subCam.rect.width;
        halfHeight *= subCam.rect.height;

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
            EventDispatcher<bool>.Dispatch(Event.PlayerUsingSpell.ToString(), true);
            GameManager.Instance.playerUsingSpell = true;
        }
    }
    private IEnumerator InvincibleSequence()
    {
        playerCollider.enabled = false;
        yield return new WaitForSeconds(1f);
        playerCollider.enabled = true;
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
