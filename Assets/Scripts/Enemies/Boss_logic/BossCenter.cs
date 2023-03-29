using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCenter : MonoBehaviour
{
    GameObject player;
    [HideInInspector] public Vector2 pos;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            Vector2 target_pos = Vector2.Lerp(transform.position, player.transform.position, 1);
            transform.position = target_pos;

            pos = transform.position;
        } 
    }
}
