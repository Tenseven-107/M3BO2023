using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int hp = 3;
    public int max_hp = 3;
    public int team = 0;

    public bool godmode = false;

    public float i_frame_time = 1;
    float last;


    public void handleHit(int damage)
    {
        if (hp > 0 && !godmode) //timeOut(i_frame_time) check if i-frames are stopped
        {
            if (Time.time - last < i_frame_time)
            {
                return;
            }
            last = Time.time;

            hp -= damage;

            if (hp <= 0)
            {
                die();
            }
        }
    }

    void die()
    {
        if (GetComponent<EntityDrawer>() != null) GetComponent<EntityDrawer>().in_screen = false;

        Destroy(gameObject);
    }
}
