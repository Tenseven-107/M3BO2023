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

    SceneLoader loader;

    private int hp_value;
    public int HPvalue
    {
        get { return hp_value; }
        set
        {
            if (hp_value != value)
            {
                hp_value = value;
                updateHP();
            }
        }
    }
    private float fuel_value;
    public float Fuelvalue
    {
        get { return hp_value; }
        set
        {
            if (fuel_value != value)
            {
                fuel_value = value;
                updateFuel();
            }
        }
    }
    private int altar_value;
    public int Altarvalue
    {
        get { return altar_value; }
        set
        {
            if (altar_value != value)
            {
                altar_value = value;
                updateAltars();
            }
        }
    }
    private int score_value;
    public int Scorevalue
    {
        get { return score_value; }
        set
        {
            score_value = value;
            updateScore();
        }
    }
    private int hi_score_value;
    public int HiScorevalue
    {
        get { return hi_score_value; }
        set
        {
            hi_score_value = value;
        }
    }

    void Start()
    {
        Invoke("Init", 0.1f);
    }

    void Init()
    {
        text = GetComponentsInChildren<TextMeshProUGUI>();
        loader = GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLoader>();

        hud.SetActive(true);
        buttons.SetActive(false);

        if (text != null)
        {
            hp = text[0];
            fuel = text[1];
            objective = text[2];
            score = text[3];
            final_score = text[4];
            hi_score = text[5];
        }

        if (gameObject.tag != "PlayerHud") gameObject.tag = "PlayerHud";
    }


    void updateHP()
    {
        hp.text = hp_value.ToString();
    }
    void updateFuel()
    {
        fuel.text = fuel_value.ToString();
    }
    void updateAltars()
    {
        objective.text = altar_value.ToString();
    }
    void updateScore()
    {
        score.text = score_value.ToString();
    }


    public void setHudDead()
    {
        hud.SetActive(false);
        buttons.SetActive(true);

        final_score.text = "Score: " + score_value.ToString();
        hi_score.text = "Hi-score: " + hi_score_value.ToString();
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
