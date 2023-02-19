using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    Transform fire_trans;
    float fire_rot;
    public GameObject bullet;


    private void Start()
    {
        fire_trans = transform;
    }


    public void fire(Vector3 fire_vec)
    {
        Instantiate(bullet, fire_trans.position, fire_trans.rotation);
    }
}
