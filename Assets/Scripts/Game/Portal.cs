using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public string next_scene_name;

    public int child_count;

    bool active = false;

    void Start()
    {
        active = false;
        if (gameObject.tag != "Portal") gameObject.tag = "Portal";
    }


    void Update()
    {
        child_count = transform.childCount + 1;

        if (child_count <= 1)
        {
            active = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && active)
        {
            SceneLoader loader = GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLoader>();
            loader.transition(next_scene_name);
        }
    }
}
