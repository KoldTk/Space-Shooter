using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private CharacterController controller;
    private Camera mainCam;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        mainCam = Camera.main;
    }

    void Update()
    {
        HandleCharacterMove();
    }

    private void HandleCharacterMove()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 move = new Vector3(horizontal, vertical).normalized;
        controller.Move(move * moveSpeed * Time.deltaTime);
        //Restrict movement
        Vector3 pos = transform.position;
        float halfHeigth = mainCam.orthographicSize;
        float halfWidth = halfHeigth * mainCam.aspect;
        pos.x = Mathf.Clamp(pos.x, -halfWidth, halfWidth);
        pos.y = Mathf.Clamp(pos.y, -halfHeigth, halfHeigth);
        transform.position = pos;

    }    

}
