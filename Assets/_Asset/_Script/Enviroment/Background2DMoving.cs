using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background2DMoving : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;
    private float spriteHeight;
    private Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
        spriteHeight = GetComponent<SpriteRenderer>().bounds.size.y;
    }
    void Update()
    {

        transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);

        if (transform.position.y <= startPosition.y - spriteHeight/2)
        {
            transform.position = startPosition;
        }
    }
}
