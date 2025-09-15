using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Camera subCam;
    [SerializeField] private GameObject magicCirclePrefab;
    [SerializeField] private GameObject spellBomb;
    [SerializeField] private GameObject normalMode;
    [SerializeField] private GameObject focusMode;
    private Vector3 startingPos = new Vector3(-3,-3.5f,0);
    private void Start()
    {
        StartCoroutine(MoveToStartPosition());
    }
    private void OnEnable()
    {
        gameObject.tag = "Player_Invi";
    }
    private void OnDisable()
    {
        if (GameManager.Instance.playerLives > 0)
        {
            EventDispatcher<bool>.Dispatch(Event.CharacterDie.ToString(), true);
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
        if (Input.GetMouseButtonDown(1))
        {
            UseSpell();
        }
        SwitchModeHandler();
    }
    private IEnumerator MoveToStartPosition()
    {   
        while (Vector3.Distance(transform.position, startingPos) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startingPos, GameManager.Instance.playerSpeed * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        gameObject.tag = "Player";
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
            Instantiate(magicCirclePrefab, transform.position, Quaternion.identity, transform);
            spellBomb.SetActive(true);
            GameManager.Instance.playerSpell--;
            EventDispatcher<bool>.Dispatch(Event.UsingSpell.ToString(), true);
            GameManager.Instance.playerUsingSpell = true;
        }
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
