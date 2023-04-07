using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    // General vars
    SpriteRenderer sprite;
    Animator anims;
    PlayerHud hud;

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
    bool active = true;

    // Combat vars
    public BulletShooter wp;
    public BulletShooter wp2;
    Entity entity;
    int hp_value;

    // FX vars
    public ParticleSystem jet_particles;
    public bool juice = true;
    [Range(0, 1)] public float boost_screenshake_time = 0.1f;
    [Range(0, 10)] public float boost_screenshake_intensity = 0.15f;
    public AudioSource boost_audio;
    public RandomAudio fuel_depleted;
    public RandomAudio fuel_up;


    // Set up
    void Start()
    {
        velocity = Vector2.zero;

        active = true;
        if (gameObject.tag != "Player") gameObject.tag = "Player";

        Init();
    }

    private void Init()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anims = GetComponent<Animator>();
        entity = GetComponent<Entity>();

        Invoke("setHUD", 1);

        jet_particles.Stop();
        anims.SetTrigger("Idle");
    }



    // Processing
    void Update()
    {
        if (entity.hp != hp_value && hud != null)
        {
            hp_value = entity.hp;
            updateHudHP();
        }
    }
    void FixedUpdate()
    {
        if (active)
        {
            movementLoop();
            combatLoop();
        }  
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
        if ((Input.GetKey("l") || Input.GetKey("space")) && (InputX != 0 || InputY != 0) && fuel > 0 && !recharging)
        {
            velocity.x *= boost_modifier;
            fuel -= 1;
            fuel = Mathf.Clamp(fuel, 0, max_fuel);

            entity.invincible = true;

            jet_particles.Emit(1);
            anims.SetTrigger("Boost");
            if (!boost_audio.isPlaying) boost_audio.Play();

            updateHudFuel();

            if (juice)
            {
                GameCamera camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameCamera>();
                camera.screenshake(boost_screenshake_time, boost_screenshake_intensity);
            }

            if (fuel <= 0) 
            {
                recharging = true;
                entity.invincible = false;

                anims.ResetTrigger("Boost");
                anims.SetTrigger("Idle");
                boost_audio.Stop();
                fuel_depleted.playSound();
            }
        }
        if (!(Input.GetKey("l") || Input.GetKey("space")) && fuel < max_fuel)
        {
            fuel += 0.5f;
            entity.invincible = false;

            anims.SetTrigger("Idle");
            boost_audio.Stop();

            updateHudFuel();

            if (fuel >= max_fuel) 
            { 
                recharging = false;
                fuel_up.playSound();
            }
        }


        // Moving the player
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }


    void flipX()
    {
        facing_right = !facing_right;
        transform.Rotate(0, 180, 0);

        if (wp.y_angle > 0) wp.y_angle -= 180;
        else wp.y_angle += 180;
    }



    // Combat input
    void combatLoop()
    {
        if (Input.GetKey("j"))
        {
            wp.fire();
        }
        if (Input.GetKey("k"))
        {
            wp2.fire();
        }
    }



    // Update hud elements
    public void setHUD()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("PlayerHud");

        if (obj != null)
        {
            hud = obj.GetComponent<PlayerHud>();

            hp_value = entity.hp;
            updateHudFuel();
            updateHudHP();
        }
    }

    void updateHudFuel()
    {
        if (hud != null)
        {
            hud.Fuelvalue = fuel;
        }
    }

    void updateHudHP()
    {
        if (hud != null)
        {
            hud.HPvalue = hp_value;
        }
    }


    // Play Win animation
    public void win()
    {
        active = false;

        anims.ResetTrigger("Idle");
        anims.ResetTrigger("Boost");
        anims.SetTrigger("Win");

        ScoreHolder holder = GameObject.FindGameObjectWithTag("ScoreHolder").GetComponent<ScoreHolder>();
        holder.submitScore(true);

        hud.setHudDead();
    }
}
