using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : MonoBehaviour
{
    Vector2 velocity;
    public float speed = 1;
    public bool right = false;

    Rigidbody2D rb;

    void Start()
    {
        velocity = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (right) velocity.x += speed * Time.fixedDeltaTime;
        else velocity.x -= speed * Time.fixedDeltaTime;

        velocity.x = Mathf.Clamp(velocity.x, -speed, speed);
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }
}
