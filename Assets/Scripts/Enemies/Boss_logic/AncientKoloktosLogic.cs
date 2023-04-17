using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AncientKoloktosLogic : MonoBehaviour
{
    bool active = false;
    bool is_attacking = false;

    int enemy_number = 4;
    const float SUMMON_TIME = 3f;
    WaitForSeconds summon_timer = new WaitForSeconds(SUMMON_TIME);

    float fire_time = 2f;

    const float ATTACK_TIME = 2.5f;
    WaitForSeconds attack_timer = new WaitForSeconds(ATTACK_TIME);

    public Turret auto_fire;
    public Turret rot_fire;
    public Turret bomb_shot;
    public Turret wave_shot;
    public Turret bomb_cirle;

    public GameObject enemy;
    public GameObject enemy_2;

    public KillBox box;
    public Area area;
    public GameObject enemy_container;
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
        IDLE,
        FIRING,
        SUMMONING,
        BOMB,
        WAVE
    }

    private States current_state;


    // Set up
    private void Start()
    {
        Init();
        switchPhase(Phases.ONE);
        switchState(States.IDLE);

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
                music.play();

                if (entity.hp < (entity.max_hp / 1.5f)) switchPhase(Phases.THREE);

                switch (current_state)
                {
                    case States.IDLE:
                        idle();
                        return;
                    case States.FIRING:
                        if (!is_attacking) StartCoroutine(fire());
                        return;
                    case States.SUMMONING: 
                        if (!is_attacking) StartCoroutine(summon());
                        return;
                    case States.BOMB:
                        if (!is_attacking) StartCoroutine(bomb());
                        return;
                }
                return;
            case Phases.THREE:
                if (entity.hp <= 1) box.Active = false;

                switch (current_state)
                {
                    case States.IDLE:
                        idle();
                        return;
                    case States.FIRING:
                        if (!is_attacking) StartCoroutine(fire());
                        return;
                    case States.SUMMONING:
                        if (!is_attacking) StartCoroutine(summon());
                        return;
                    case States.BOMB:
                        if (!is_attacking) StartCoroutine(bomb());
                        return;
                    case States.WAVE:
                        if (!is_attacking) StartCoroutine(waveFire());
                        return;
                }
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


    // States
    IEnumerator summon()
    {
        is_attacking = true;
        yield return summon_timer;

        int spawns = enemy_number + (int)Mathf.Round((0.5f / entity.hp) * 100);
        for (int i = 0; i <= spawns; ++i)
        {
            if (enemy_container.transform.childCount < 12)
            {
                Vector2 original_pos = enemy_container.transform.position;
                float X = original_pos.x + Random.Range(-0.5f, 0.5f);
                float Y = original_pos.y + Random.Range(-0.5f, 0.5f);
                Vector2 pos = new Vector2(X, Y);

                if (current_phase == Phases.TWO) Instantiate(enemy, pos, Quaternion.Euler(0, 0, 0), enemy_container.transform);
                else Instantiate(enemy_2, pos, Quaternion.Euler(0, 0, 0), enemy_container.transform);
            }
        }
        switchState(States.IDLE);
        yield break;
    }

    IEnumerator fire()
    {
        is_attacking = true;
        if (current_phase == Phases.TWO) auto_fire.active = true;
        else rot_fire.active = true;

        float time = fire_time + Mathf.Round((0.25f / entity.hp) * 100);
        yield return new WaitForSeconds(time);
        if (current_phase == Phases.TWO) auto_fire.active = false;
        else rot_fire.active = false;
        switchState(States.IDLE);
        yield break;
    }

    IEnumerator waveFire()
    {
        is_attacking = true;
        wave_shot.active = true;
        yield return attack_timer;
        wave_shot.active = false;
        switchState(States.IDLE);
        yield break;
    }

    IEnumerator bomb()
    {
        is_attacking = true;
        if (current_phase == Phases.TWO) bomb_shot.active = true;
        else bomb_cirle.active = true;
        yield return attack_timer;
        if (current_phase == Phases.TWO) bomb_shot.active = false;
        else bomb_cirle.active = false;
        switchState(States.IDLE);
        yield break;
    }

    void idle()
    {
        is_attacking = false;
        if (current_phase == Phases.TWO)
        {
            anims.SetTrigger("PhaseTwo");

            int number = Random.Range(0, 3);
            if (number == 0) switchState(States.FIRING);
            else if (number == 1) switchState(States.SUMMONING);
            else switchState(States.BOMB);
        }
        else if (current_phase == Phases.THREE)
        {
            anims.SetTrigger("PhaseThree");

            int number = Random.Range(0, 4);
            if (number == 0) switchState(States.FIRING);
            else if (number == 1) switchState(States.SUMMONING);
            else if (number == 2) switchState(States.BOMB);
            else switchState(States.WAVE);
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
