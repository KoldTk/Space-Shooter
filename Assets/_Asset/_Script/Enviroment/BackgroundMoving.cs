using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMoving : MonoBehaviour
{
    private Vector2 startPosition;
    public float loopPosition;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2 (transform.position.x, transform.position.y - speed * Time.deltaTime);
        if (transform.position.y <= loopPosition)
        {
            BackgroundLoop();
        }    
    }

    public void BackgroundLoop()
    {
        transform.position = startPosition;
    }    
}
