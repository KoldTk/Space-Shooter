using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMoving : MonoBehaviour
{
    public Renderer bgRenderer;
    public float speed;
    // Update is called once per frame
    void Update()
    {
        bgRenderer.material.mainTextureOffset = new Vector2(0, Time.time * speed);
    }
}
