using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Camera subCam;
    [SerializeField] private GameObject spellPrefab;
    private Vector3 startingPos = new Vector3(-3,-3.5f,0);
    private int powerMileStone = 16;
    private int gunStage = 0;
    private bool isUsingSpell;
    private List<Transform> guns = new List<Transform>();
    private void Start()
    {
        StartCoroutine(MoveToStartPosition());
    }
    private void OnEnable()
    {
        gameObject.tag = "Player_Invi";
        foreach( Transform child in transform)
        {
            guns.Add(child);
        }    
        EventDispatcher<bool>.AddListener(Event.StatusChange.ToString(), LevelUp);
    }
    private void OnDisable()
    {
        EventDispatcher<bool>.RemoveListener(Event.StatusChange.ToString(), LevelUp);
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
    }
    private IEnumerator MoveToStartPosition()
    {   
        while (Vector3.Distance(transform.position, startingPos) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startingPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        gameObject.tag = "Player";
    }    
    private void LevelUp(bool isChanged)
    {
        //Power up if gain enough power point
        if (GameManager.Instance.playerPower >= powerMileStone)
        {
            powerMileStone *= 2;
            gunStage++;
            ChangeGunStage();
        }    
    }
    private void ChangeGunStage()
    {
        for (int i = 0; i <= gunStage; i++)
        {
            guns[i].gameObject.SetActive(true);
        }
    }    
    private void HandleCharacterMove()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 move = new Vector3(horizontal, vertical, 0).normalized;
        transform.Translate(move * moveSpeed * Time.deltaTime);
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
            Instantiate(spellPrefab, transform.position, Quaternion.identity, transform);
            GameManager.Instance.playerSpell--;
            EventDispatcher<bool>.Dispatch(Event.UsingSpell.ToString(), true);
            GameManager.Instance.playerUsingSpell = true;
        }
    }    
}
