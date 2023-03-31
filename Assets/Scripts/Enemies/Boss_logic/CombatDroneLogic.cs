using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class CombatDroneLogic : MonoBehaviour
{
    const float SPEED = 55;
    const float AMPLITUDE = 0.5f;
    const float FREQUENCY = 6;
    const float ATTACK_DISTANCE = 2.85f;

    float last;
    const float IDLE_TIME = 2f;
    WaitForSeconds idle_timer = new WaitForSeconds(IDLE_TIME);
    bool is_idle = false;

    const float ATTACK_TIME = 0.6f;
    WaitForSeconds attack_timer = new WaitForSeconds(ATTACK_TIME);
    bool is_attacking = false;

    public BulletShooter auto_fire;
    public BulletShooter big_shot;
    public BulletShooter power_shot;
    public BulletShooter area_shot;

    BossCenter center;
    Rigidbody2D rb;
    Entity entity;

    Animator anims;


    // State variables
    private enum States
    { 
        IDLE,
        MOVE,
        FLIP,
        TURN,
        POWERSHOT,
        DIE
    }

    private States current_state;



    // Set up
    private void Start()
    {
        Init();
        switchState(States.IDLE);
    }

    void Init()
    {
        rb = GetComponent<Rigidbody2D>();
        center = GetComponentInChildren<BossCenter>();
        entity = GetComponent<Entity>();
        anims = GetComponent<Animator>();
    }


    // Process
    private void FixedUpdate()
    {
        Vector2 relative = transform.InverseTransformPoint(center.pos);
        float angle = (Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg) + 90;
        transform.Rotate(0, 0, -angle);

        if (current_state != States.MOVE) rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, 0.1f);
        if (current_state != States.IDLE && !(entity.hp < entity.max_hp)) entity.invincible = true;
        else entity.invincible = false;

        stateMachine();
    }


    // Statemachine
    void stateMachine()
    {
        switch (current_state)
        {
            case States.IDLE:
                anims.SetTrigger("Idle");
                if (!is_idle) StartCoroutine(idle());
                return;
            case States.MOVE:
                move();
                return;
            case States.FLIP:
                if (!is_attacking)
                {
                    anims.SetTrigger("Flip");
                    StartCoroutine(attack());
                }
                return;
            case States.TURN:
                if (!is_attacking)
                {
                    anims.SetTrigger("Turn");
                    StartCoroutine(attack());
                }
                return;
            case States.POWERSHOT:
                if (!is_attacking)
                {
                    anims.SetTrigger("Powershot");
                    StartCoroutine(secondAttack());
                }
                return;
            case States.DIE:
                return;
        }
    }

    private void switchState(States state)
    {
        if (state == current_state)
        {
            return;
        }
        else
        {
            current_state = state;
        }
    }


    // States
    IEnumerator idle()
    {
        is_idle = true;
        yield return idle_timer;
        switchState(States.MOVE);
        yield break;
    }

    IEnumerator attack()
    {
        is_attacking = true;
        yield return attack_timer;
        big_shot.fire();
        switchState(States.IDLE);
        yield break;
    }

    IEnumerator secondAttack()
    {
        is_attacking = true;
        yield return attack_timer;
        power_shot.fire();
        switchState(States.IDLE);
        yield break;
    }


    void move()
    {
        anims.SetTrigger("Fly");

        is_idle = false;
        is_attacking = false;

        Vector2 pos = rb.velocity;
        float sine = Mathf.Sin(pos.x * FREQUENCY) * AMPLITUDE;
        pos = new Vector2(-transform.up.x + sine, -transform.up.y + sine);
        rb.velocity = pos * SPEED * Time.fixedDeltaTime;

        float distance = Vector2.Distance(rb.position, center.pos);
        if (distance < ATTACK_DISTANCE && entity.hp >= entity.max_hp / 2)
        {
            auto_fire.fire();
        }
        else if (distance < ATTACK_DISTANCE && entity.hp < entity.max_hp / 2)
        {
            area_shot.fireCircle();
        }
        else if (distance > ATTACK_DISTANCE && entity.hp >= entity.max_hp / 2)
        {
            int num = UnityEngine.Random.Range(0, 2);

            if (num == 0)
            {
                switchState(States.FLIP);
            }
            else switchState(States.TURN);
        }
        else if (distance > ATTACK_DISTANCE / 2 && entity.hp < entity.max_hp / 2)
        { 
            switchState(States.POWERSHOT);
        }
    }


    // Time
    float getCurrentAnimTime()
    {
        float current_anim_time = anims.GetCurrentAnimatorStateInfo(0).normalizedTime;
        return current_anim_time;
    }

    // I hate this boss, gonna be honest, f*ck him
}
