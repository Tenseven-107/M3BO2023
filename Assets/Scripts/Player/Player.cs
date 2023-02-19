using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    // General vars
    SpriteRenderer sprite;

    // Movement vars
    Vector2 velocity;
    public float max_speed = 5;
    public float accel = 0.75f;
    Rigidbody2D rb;

    public float fuel = 100;
    public float max_fuel = 100;
    public float boost_modifier = 2.5f;
    bool recharging = false;

    bool facing_right = true;

    // Combat vars
    public int hp = 3;
    public int max_hp = 3;
    public int team = 0;

    public float i_frame_time = 1;
    public float dodge_time = 0.5f;
    float lastTimeout;

    GameObject wp;

    
    // Set up
    void Start()
    {
        Init();
    }

    private void Init()
    {
        velocity = Vector2.zero;

        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        wp = transform.GetChild(0).gameObject;
    }



    // Processing
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        movementLoop();
    }



    // Movement
    void movementLoop()
    {
        float InputX = Input.GetAxisRaw("Horizontal");
        float InputY = Input.GetAxisRaw("Vertical");
        
        // Setting movement
        if (InputX != 0 || InputY != 0)
        {
            velocity.x += InputX * accel;
            velocity.y += InputY * accel;

            velocity.x = Mathf.Clamp(velocity.x, -max_speed, max_speed);
            velocity.y = Mathf.Clamp(velocity.y, -max_speed, max_speed);

            if (velocity.x > 0 && !facing_right)
            {
                flipX();
            }
            else if (velocity.x < 0 && facing_right)
            {
                flipX();
            }
        } 
        else
        {
            velocity = Vector2.zero;
        }


         // Boosting
        if (Input.GetKey("l") && fuel > 0 && !recharging)
        {
            velocity.x *= boost_modifier;
            fuel -= 1;

            fuel = Mathf.Clamp(fuel, 0, max_fuel);
            if (fuel <= 0) 
            {
                recharging = true;
            }
        }
        if (fuel < max_fuel)
        {
            fuel += 0.5f;
            if (fuel >= max_fuel) 
            { 
                recharging = false;
            }
        }


        // Moving the player
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }


    void flipX()
    {
        facing_right = !facing_right;
        transform.Rotate(0, 180, 0);
    }


    // Handle hits
    public void handleHit(int damage, int damage_team)
    {
        if (damage_team != team && hp > 0) //timeOut(i_frame_time) && timeOut(dodge_time)) check if i-frames and dodge are stopped
        {
            hp -= damage;

            if (hp <= 0)
            {
                die();
            }
        }
    }

    void die()
    {
        // Player dies
    }



    // Timer time out
    // make timer
    void timeOut(float time)
    {
        if (Time.time - lastTimeout < time)
        {
            return;
        }
        lastTimeout = Time.time;
    }
}
