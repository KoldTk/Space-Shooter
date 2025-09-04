using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Camera subCam;
    private int powerMileStone = 16;
    private int gunStage = 0;
    private List<Transform> guns = new List<Transform>();

    private void OnEnable()
    {
        foreach( Transform child in transform)
        {
            guns.Add(child);
        }    
        EventDispatcher<bool>.AddListener(Event.StatusChange.ToString(), LevelUp);
    }
    private void OnDisable()
    {
        EventDispatcher<bool>.RemoveListener(Event.StatusChange.ToString(), LevelUp);
    }

    void Update()
    {
        HandleCharacterMove();
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

        transform.position = pos;
    }
    private void RestrictMovement()
    {

    }    

}
