using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public Renderer bgRenderer;
    public float scrollSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bgRenderer.material.mainTextureOffset = new Vector2(0, Time.time * scrollSpeed);
    }
}
