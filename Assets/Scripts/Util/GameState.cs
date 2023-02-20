using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public bool is_paused = false;
    public GameObject pause;


    void Update()
    {
        getInput();
    }


    void getInput()
    {
        if (Input.GetKeyDown("escape") && !is_paused)
        {
            is_paused = true;
            GameObject pause_screen = Instantiate(pause, transform.position, transform.rotation);
            pause_screen.transform.parent = transform;
        }
    }
}
