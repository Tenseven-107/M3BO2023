using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // - En hier maken we de functie van bewegen afzonderlijk ! Nu voldoet het wel aan SRP

    // Movement vars
    public Vector2 velocity;
    public float max_speed = 5;
    public float accel = 0.75f;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MovementLoop();
    }

    // Movement
    void MovementLoop()
    {
        // Movement controls
        float InputX = Input.GetAxisRaw("Horizontal");
        float InputY = Input.GetAxisRaw("Vertical");

        if (Input.GetJoystickNames().Length > 0)
        {
            if (Input.GetJoystickNames()[0] != "")
            {
                InputX = Input.GetAxis("MoveHorizontal");
                InputY = Input.GetAxis("MoveVertical");
            }
        }

        // Setting movement
        if (InputX != 0 || InputY != 0)
        {
            velocity.x += InputX * accel;
            velocity.y += InputY * accel;

            velocity.x = Mathf.Clamp(velocity.x, -max_speed, max_speed);
            velocity.y = Mathf.Clamp(velocity.y, -max_speed, max_speed);
        }

        // Moving the player
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }
}
