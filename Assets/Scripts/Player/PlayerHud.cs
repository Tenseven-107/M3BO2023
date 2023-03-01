using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHud : MonoBehaviour
{
    TextMeshProUGUI[] text;

    TextMeshProUGUI hp;
    TextMeshProUGUI fuel;
    TextMeshProUGUI objective;
    TextMeshProUGUI score;

    Player player;
    Entity player_entity;
    ScoreHolder holder;
    Portal portal;


    void Start()
    {
        text = GetComponentsInChildren<TextMeshProUGUI>();
        
        if (text != null )
        {
            hp = text[0];
            fuel = text[1];
            objective = text[2];
            score = text[3];
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
            objective.text = (portal.child_count - 1).ToString();
            score.text = holder.score.ToString();
        }
    }
}
