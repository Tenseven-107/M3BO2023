using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string start_scene_name = "SampleScene";
    public GameObject options_menu;


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
