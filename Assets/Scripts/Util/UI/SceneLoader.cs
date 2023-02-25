using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public float load_time = 1;
    public string scene_name = "TestScene";
    public bool loading = false;

    Animator anims;


    private void Start()
    {
        if (gameObject.tag != "SceneLoader") gameObject.tag = "SceneLoader";
        anims = GetComponentInChildren<Animator>();

        loading = false;
    }


    public void transition()
    {
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
        SceneManager.LoadScene(scene_name);
    }


    // MESSAGE FOR FUTURE ME:    - Replace null text with objective
    void Update() // REMOVE LATER
    {
        if (Input.GetKeyDown("y"))
        {
            transition(); // TEST FUNCTIONALITY. CHANGE LATER
        }
    }
}
