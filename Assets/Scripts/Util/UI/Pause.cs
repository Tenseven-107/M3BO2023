using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public float unpause_time = 4;

    GameObject state;
    TextMeshProUGUI text;
    GameState game_state;


    private void Start()
    {
        Time.timeScale = 0.0f;

        state = GameObject.FindGameObjectWithTag("GameState");
        game_state = state.GetComponent<GameState>();

        text = gameObject.transform.Find("Canvas/Text").gameObject.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            StartCoroutine(WaitAndUnpause());
        }
    }


    IEnumerator WaitAndUnpause()
    {
        float time = unpause_time;
        while (time > 0.1)
        {
            time -= 0.1f * Time.fixedDeltaTime;
            int time_int = (int)time;
            text.text = time_int.ToString();

            yield return null;
        }
        unpause();
    }


    void unpause()
    {
        Time.timeScale = 1;
        game_state.is_paused = false;

        Destroy(gameObject);
    }
}
