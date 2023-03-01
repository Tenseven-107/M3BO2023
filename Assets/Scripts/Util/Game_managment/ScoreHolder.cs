using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;


public class ScoreHolder : MonoBehaviour
{
    public int score = 0;
    public int hi_score = 0;
    string path;


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

        if (gameObject.tag != "ScoreHolder") gameObject.tag = "ScoreHolder";
    }


    public void addScore(int added_score) { score += added_score; }


    public void submitScore(bool reset)
    {
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
}
