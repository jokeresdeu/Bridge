using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedLimit : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField]float maxSpeed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Mathf.Abs(rb.velocity.y) > maxSpeed)
            rb.velocity = new Vector2(rb.velocity.x, maxSpeed);
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
            rb.velocity = new Vector2(maxSpeed,rb.velocity.y);
    }
}
