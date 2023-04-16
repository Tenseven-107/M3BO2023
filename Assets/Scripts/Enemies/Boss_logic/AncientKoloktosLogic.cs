using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AncientKoloktosLogic : MonoBehaviour
{
    bool active = false;

    int enemy_number = 2;
    const float SUMMON_TIME = 3f;
    WaitForSeconds idle_timer = new WaitForSeconds(SUMMON_TIME);
    bool is_summoning = false;

    float fire_time = 2f;
    bool is_firing = false;

    const float ATTACK_TIME = 0.6f;
    WaitForSeconds attack_timer = new WaitForSeconds(ATTACK_TIME);
    bool is_attacking = false;

    public BulletShooter auto_fire;
    public BulletShooter bomb_shot;
    public BulletShooter wave_shot;
    public BulletShooter bomb_cirle;

    public GameObject enemy;
    public GameObject enemy_2;

    public KillBox box;
    public Area area;
    public GameObject altar_container;
    int altars = 0;

    Entity entity;
    Animator anims;
    MusicSetter music;


    // State variables
    private enum Phases
    {
        ONE,
        TWO, 
        THREE
    }

    private Phases current_phase;

    private enum States
    {
        FIRING,
        SUMMONING,

    }

    private States current_state;


    // Set up
    private void Start()
    {
        Init();
        switchPhase(Phases.ONE);
        switchState(States.SUMMONING);

        if (altar_container != null) altars = altar_container.transform.childCount;
        entity.invincible = true;
    }

    void Init()
    {
        entity = GetComponent<Entity>();
        anims = GetComponent<Animator>();
        music = GetComponent<MusicSetter>();
    }


    // Statemachine
    private void Update()
    {
        stateMachine();
    }

    void stateMachine()
    {
        switch (current_phase)
        {
            case Phases.ONE:
                if (checkAltars())
                {
                    box.Active = true;
                    if (area.is_colliding)
                    {
                        switchPhase(Phases.TWO);
                    } 
                }
                    
                return;
            case Phases.TWO:
                entity.invincible = false;
                anims.SetTrigger("PhaseTwo");
                music.play();

                switch (current_state)
                {
                }

                if (entity.hp < (entity.max_hp / 1.5f)) switchPhase(Phases.THREE);

                return;
            case Phases.THREE:
                anims.SetTrigger("PhaseThree");

                switch (current_state)
                {
                }

                if (entity.hp <= 1) box.Active = false;

                return;
        }
    }

    private void switchPhase(Phases phase)
    {
        if (phase == current_phase)
        {
            return;
        }
        else
        {
            current_phase = phase;
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

    // Check altars
    bool checkAltars()
    {
        if (altar_container != null) altars = altar_container.transform.childCount;

        if (altars <= 0) return true;
        else return false;
    }
}
