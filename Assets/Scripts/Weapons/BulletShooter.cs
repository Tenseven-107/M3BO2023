using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    public float cooldown = 0.1f;
    float last;

    public int bullets = 1;

    public bool has_spread = true;
    public float spread = 0;

    Transform fire_trans;

    public float y_angle = 0;

    public GameObject bullet;


    private void Start()
    {
        fire_trans = transform;
    }


    public void fire()
    {
        if (Time.time - last < cooldown)
        {
            return;
        }
        last = Time.time;

        for (int i = 0; i < bullets; i++)
        {
            if (has_spread)
            {
                float number = UnityEngine.Random.RandomRange(-spread, spread);
                float angle = number;
                if (!(fire_trans.eulerAngles.z < -spread || fire_trans.eulerAngles.z > spread)) fire_trans.transform.eulerAngles = new Vector3(0, y_angle, angle);
                else fire_trans.transform.eulerAngles = new Vector3(0, y_angle, 0);
            }

            Instantiate(bullet, fire_trans.position, fire_trans.rotation);
        }
    }
}
