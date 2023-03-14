using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHud : MonoBehaviour
{
    public GameObject hud;
    public GameObject buttons;

    TextMeshProUGUI[] text;

    TextMeshProUGUI hp;
    TextMeshProUGUI fuel;
    TextMeshProUGUI objective;
    TextMeshProUGUI score;

    TextMeshProUGUI final_score;
    TextMeshProUGUI hi_score;

    Player player;
    Entity player_entity;
    ScoreHolder holder;
    Portal portal;

    SceneLoader loader;


    void Start()
    {
        text = GetComponentsInChildren<TextMeshProUGUI>();
        loader = GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLoader>();

        hud.active = true;
        buttons.active = false;
        
        if (text != null )
        {
            hp = text[0];
            fuel = text[1];
            objective = text[2];
            score = text[3];
            final_score = text[4];
            hi_score = text[5];
        }

        Invoke("Init", 0.01f);
    }

    private void Init()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player_entity = GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>();
        holder = GameObject.FindGameObjectWithTag("ScoreHolder").GetComponent<ScoreHolder>();
        portal = GameObject.FindGameObjectWithTag("Portal").GetComponent<Portal>();
    }


    void Update()
    {
        if (holder != null && player != null)
        {
            hp.text = player_entity.hp.ToString();
            fuel.text = player.fuel.ToString();
            objective.text = portal.child_count.ToString();
            score.text = holder.score.ToString();

            if (player_entity.hp <= 0)
            {
                setHudDead();
            }
        }
    }

    void setHudDead()
    {
        buttons.active = true;
        hud.active = false;

        final_score.text = "Score: " + holder.score_log.ToString();
        hi_score.text = "Hi-score: " + holder.hi_score.ToString();
    }


    public void buttonRetry()
    {
        loader.transition("");
    }

    public void buttonQuit()
    {
        loader.transition("Main_menu");
    }
}
