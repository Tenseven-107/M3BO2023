using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float cooldown = 10f;
    float last;

    public bool move_with_camera = false;
    public float x_position = 4;
    GameObject gc;

    public GameObject[] enemies;
    Transform[] spawn_points;


    void Start()
    {
        spawn_points = GetComponentsInChildren<Transform>();

        gc = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void Update()
    {
        spawn();

        if (move_with_camera) setPos();
    }


    void spawn()
    {
        if (Time.time - last < cooldown)
        {
            return;
        }
        last = Time.time;

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
