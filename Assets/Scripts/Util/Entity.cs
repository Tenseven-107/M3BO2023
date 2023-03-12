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
    SpriteRenderer sprite;

    public GameObject death_effect;

    [Range(0, 0.1f)] public float hitstop_time = 0;


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

            if (hitstop_time > 0)
            {
                GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
                camera.GetComponent<GameCamera>().hitstop(hitstop_time);
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
        yield return new WaitForSeconds(0.05f);

        for (float n = 0; n < i_frame_time; n += 0.1f)
        {
            if (flash)
            {
                sprite.color = Color.clear;
                yield return new WaitForSeconds(0.025f);
                sprite.color = color;
                yield return new WaitForSeconds(0.025f);
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
        else gameObject.SetActive(false);
    }
}
