using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    GameObject state;
    GameState game_state;

    void Start()
    {
        Time.timeScale = 0.0f;

        state = GameObject.FindGameObjectWithTag("GameState");
        game_state = state.GetComponent<GameState>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            unpause();
        }
    }

    void unpause()
    {
        Time.timeScale = 1;
        game_state.is_paused = false;

        Destroy(gameObject);
    }
}
