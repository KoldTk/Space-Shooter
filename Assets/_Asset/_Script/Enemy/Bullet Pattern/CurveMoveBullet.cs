using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveMoveBullet : MonoBehaviour
{
    private float speed;
    public float curveStrength = 100f;
    public float curveDuration = 0.5f;
    public bool curveRight = true;

    private Rigidbody2D rb;
    private Vector2 initialDir;
    private float curveTimer;
    private bool isCurving = true;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialDir = rb.velocity.normalized;
        curveTimer = 0f;
        
    }
    private void Update()
    {
        if (rb == null) return;
        speed = rb.velocity.magnitude;
        if (isCurving)
        {
            curveTimer += Time.deltaTime;
            float angle = (curveRight ? 1f : -1f) * curveStrength * Time.deltaTime;

            Vector2 newDir = Quaternion.Euler(0, 0, angle) * rb.velocity.normalized;
            rb.velocity = newDir * speed;

            if (curveTimer >= curveDuration)
            {
                isCurving = false;
                rb.velocity = initialDir * speed;  
            }
        }
        else
        {
            //Back to start direction
            rb.velocity = initialDir * speed;
        }
    }
}
