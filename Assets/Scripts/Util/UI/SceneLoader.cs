using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public float load_time = 1;
    public string scene_name = "TestScene";
    string scene_name_override = "";
    public bool loading = false;

    Animator anims;


    private void Start()
    {
        if (gameObject.tag != "SceneLoader") gameObject.tag = "SceneLoader";
        anims = GetComponentInChildren<Animator>();

        loading = false;
    }


    public void transition(string override_name)
    {
        if (override_name != "") scene_name_override = override_name;

        StartCoroutine(load());

        if (GameObject.FindGameObjectWithTag("ScoreHolder"))
        {
            ScoreHolder holder = GameObject.FindGameObjectWithTag("ScoreHolder").GetComponent<ScoreHolder>();
            if (holder != null) holder.submitScore(false);
        }
    }


    IEnumerator load()
    {
        anims.SetTrigger("start");
        loading = true;

        yield return new WaitForSeconds(load_time);

        if (scene_name_override == "") SceneManager.LoadScene(scene_name);
        else SceneManager.LoadScene(scene_name_override);
    }


    // MESSAGE FOR FUTURE ME:    - Replace null text with objective
    void Update() // REMOVE LATER
    {
        if (Input.GetKeyDown("y"))
        {
            transition(""); // TEST FUNCTIONALITY. CHANGE LATER
        }
    }
}
