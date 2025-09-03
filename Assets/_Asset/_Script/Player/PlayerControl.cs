using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Camera subCam;
    private Rigidbody2D body2D;
    
    void Start()
    {
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

}
