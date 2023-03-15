using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreHolder : MonoBehaviour
{
    public string score_reset_scene = "Level_1";
    [HideInInspector] public int score = 0;
    [HideInInspector] public int hi_score = 0;
    string path;

    PlayerHud hud;


    private void Start()
    {
        path = Path.Combine(Application.persistentDataPath, "Save.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            ScoreSave save = JsonUtility.FromJson<ScoreSave>(json);

            score = save.score;
            hi_score = save.hi_score;
        }
        else
        {
            writeFile();
        }

        Invoke("setHUD", 1);

        if (gameObject.tag != "ScoreHolder") gameObject.tag = "ScoreHolder";

        if (SceneManager.GetActiveScene().name == score_reset_scene)
        {
            score = 0;
            writeFile();
        }
    }


    public void addScore(int added_score) 
    { 
        score += added_score;
        setHudScore();
    }


    public void submitScore(bool reset)
    {
        setHudScore();
        setHudHiScore();

        if (score > hi_score) hi_score = score;
        if (reset) score = 0;

        writeFile();
    }


    private void writeFile()
    {
        ScoreSave save = new ScoreSave();
        save.score = score;
        save.hi_score = hi_score;

        string json = JsonUtility.ToJson(save);

        File.WriteAllText(path, json);
    }


    void setHUD()
    {
        hud = GameObject.FindGameObjectWithTag("PlayerHud").GetComponent<PlayerHud>();

        setHudScore();
    }

    void setHudScore()
    {
        if (hud != null)
        {
            hud.Scorevalue = score;
        }
    }

    void setHudHiScore()
    {
        if (hud != null)
        {
            hud.HiScorevalue = hi_score;
        }
    }
}
