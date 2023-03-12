using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Range(5, 20)] public float cooldown = 10f;
    float last;

    public bool move_with_camera = false;
    public float x_position = 4;
    GameObject gc;

    public GameObject[] enemies;
    Transform[] spawn_points;

    public bool active = true;


    void Start()
    {
        spawn_points = GetComponentsInChildren<Transform>();
        gc = GameObject.FindGameObjectWithTag("MainCamera");

        if (gameObject.tag != "SpawnManager") gameObject.tag = "SpawnManager";

        if (active) StartCoroutine(cycle());
    }

    private void Update()
    {
        if (move_with_camera) setPos();
    }


    IEnumerator cycle()
    {
        while (active)
        {
            yield return new WaitForSeconds(cooldown);
            spawn();
        }
        if (active) yield break;
    }


    void spawn()
    {
        int children = transform.childCount + 1;
        int child_number = Mathf.Clamp(UnityEngine.Random.Range(0, children), 1, children);
        int enemy_number = UnityEngine.Random.Range(0, enemies.Length);

        Transform spawn_pos = spawn_points[child_number];
        Transform parent = transform.parent.transform;

        Instantiate(enemies[enemy_number], spawn_pos.position, Quaternion.Euler(0, 0, 0), parent);
    }

    void setPos()
    {
        float posX = gc.transform.position.x + x_position;

        transform.position = new Vector3(posX, gc.transform.position.y, 0);
    }
}
