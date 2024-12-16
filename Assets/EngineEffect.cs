using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineEffect : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        spriteRenderer.enabled = !spriteRenderer.enabled;
    }

}
