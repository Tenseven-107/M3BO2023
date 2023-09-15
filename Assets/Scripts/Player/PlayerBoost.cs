using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerBoost : MonoBehaviour
{
    public float fuel = 100;
    public float max_fuel = 100;
    public float boost_modifier = 2.5f;
    bool recharging = false;

    public ParticleSystem jet_particles;
    public bool juice = true;
    [Range(0, 1)] public float boost_screenshake_time = 0.1f;
    [Range(0, 10)] public float boost_screenshake_intensity = 0.15f;
    public AudioSource boost_audio;
    public RandomAudio fuel_depleted;
    public RandomAudio fuel_up;

    Vector2 velocity;
    PlayerMovement movement;
    Entity entity;
    Animator anims;


    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
        entity = GetComponent<Entity>();
        anims = GetComponent<Animator>();
    }


    private void FixedUpdate()
    {
        velocity = movement.velocity;
        BoostLoop();
    }

    private void BoostLoop()
    {
        // Boosting
        if (Input.GetButton("Boost") && velocity != Vector2.zero && fuel > 0 && !recharging)
        {
            velocity.x *= boost_modifier;
            fuel -= 1;
            fuel = Mathf.Clamp(fuel, 0, max_fuel);

            entity.invincible = true;

            jet_particles.Emit(1);
            anims.SetTrigger("Boost");
            if (!boost_audio.isPlaying) boost_audio.Play();

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
        if ((!Input.GetButton("Boost") || velocity == Vector2.zero) && fuel < max_fuel)
        {
            fuel += 0.5f;
            entity.invincible = false;

            anims.SetTrigger("Idle");
            boost_audio.Stop();

            if (fuel >= max_fuel)
            {
                recharging = false;
                fuel_up.playSound();
            }
        }

        movement.velocity = velocity;
    }
}
