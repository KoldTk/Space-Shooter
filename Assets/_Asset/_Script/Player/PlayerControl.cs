using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Camera mainCam;
    private Rigidbody2D body2D;
    
    void Start()
    {
        mainCam = Camera.main;
        body2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleCharacterMove();
    }

    private void HandleCharacterMove()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 move = new Vector3(horizontal, vertical, 0).normalized;
        transform.Translate(move * moveSpeed * Time.deltaTime);
        //Restrict movement
        Vector3 pos = transform.position;
        float halfHeigth = mainCam.orthographicSize;
        float halfWidth = halfHeigth * mainCam.aspect;
        pos.x = Mathf.Clamp(pos.x, -halfWidth, halfWidth);
        pos.y = Mathf.Clamp(pos.y, -halfHeigth, halfHeigth);
        transform.position = pos;

    }    

}
