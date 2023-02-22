using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float cooldown = 10f;
    float last;

    public GameObject[] enemies;
    Transform[] spawn_points;


    void Start()
    {
        spawn_points = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        spawn();
    }


    void spawn()
    {
        if (Time.time - last < cooldown)
        {
            return;
        }
        last = Time.time;

        int children = transform.childCount + 1;
        int child_number = Mathf.Clamp(Random.Range(0, children), 1, children);
        int enemy_number = Random.Range(0, enemies.Length);

        Transform spawn_pos = spawn_points[child_number];

        Instantiate(enemies[enemy_number], spawn_pos);
    }
}
