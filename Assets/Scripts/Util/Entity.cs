using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int hp = 3;
    public int max_hp = 3;
    public int team = 0;

    public bool godmode = false;
    [HideInInspector] public bool invincible = false;

    public float i_frame_time = 1;
    float last;

    public bool npc = true;
    public int score = 5;

    bool flash = true;
    WaitForSeconds flash_timer = new WaitForSeconds(0.05f);
    WaitForSeconds flash_timer_short = new WaitForSeconds(0.025f);
    SpriteRenderer sprite;
    public GameObject death_effect;

    public bool juice = false;
    [Range(0, 0.1f)] public float hitstop_time = 0;
    [Range(0, 1)] public float screenshake_time = 0;
    [Range(0, 10)] public float screenshake_intensity = 0;


    private void Start()
    {
        flash = true;
        sprite = GetComponent<SpriteRenderer>();
    }


    public void handleHit(int damage)
    {
        if (hp > 0 && !godmode && !invincible)
        {
            if (Time.time - last < i_frame_time)
            {
                return;
            }
            last = Time.time;

            hp -= damage;
            StartCoroutine(Flash());

            if (juice)
            {
                GameCamera camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameCamera>();
                camera.hitstop(hitstop_time);
                camera.screenshake(screenshake_time, screenshake_intensity);
            }

            if (hp <= 0)
            {
                flash = false;
                die();
            }
        }
    }


    IEnumerator Flash()
    {
        Color color = sprite.color;

        sprite.color = Color.red;
        yield return flash_timer;

        for (float n = 0; n < i_frame_time; n += 0.1f)
        {
            if (flash)
            {
                sprite.color = Color.clear;
                yield return flash_timer_short;
                sprite.color = color;
                yield return flash_timer_short;
            }
            else break;
        }
    }


    void die()
    {
        if (GetComponent<EntityDrawer>() != null) GetComponent<EntityDrawer>().in_screen = false;

        ScoreHolder holder = GameObject.FindGameObjectWithTag("ScoreHolder").GetComponent<ScoreHolder>();
        if (!npc) holder.submitScore(true);
        else holder.addScore(score);

        if (death_effect != null)
        {
            Transform parent = transform.parent;
            Instantiate(death_effect, transform.position, Quaternion.Euler(0, 0, 0), parent);
        }

        if (npc) Destroy(gameObject);
        else setInactive();
    }

    void setInactive()
    {
        gameObject.SetActive(false);

        PlayerHud hud = GameObject.FindGameObjectWithTag("PlayerHud").GetComponent<PlayerHud>();
        if (hud != null)
        {
            hud.setHudDead();
        }
    }
}
