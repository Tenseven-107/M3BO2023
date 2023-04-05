using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SocialPlatforms;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public string start_scene_name = "Level_1";
    public GameObject options_menu;

    public TextMeshProUGUI hi_score_text;
    string path;

    private void Start()
    {
        path = Path.Combine(Application.persistentDataPath, "Save.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            ScoreSave save = JsonUtility.FromJson<ScoreSave>(json);
            hi_score_text.text = "Hi-score: " + save.hi_score.ToString();
        }
        else hi_score_text.text = "";
    }


    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            StartGame();
        }
        if (Input.GetKeyDown("escape")) 
        { 
            Quit();
        }
    }


    public void StartGame()
    {
        SceneManager.LoadScene(start_scene_name);
    }

    public void Options()
    {
        gameObject.SetActive(false);
        options_menu.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
