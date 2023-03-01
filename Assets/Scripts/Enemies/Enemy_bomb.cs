using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_bomb : MonoBehaviour
{
    BulletShooter wp;
    Area area;


    void Start()
    {
        wp = GetComponentInChildren<BulletShooter>();
        area = GetComponentInChildren<Area>();

        GetComponent<SpriteRenderer>().color = Random.ColorHSV(0, 1, 1, 1, 1, 1);
    }


    void Update()
    {
        if (area.is_colliding)
        {
            wp.fireCircle();
            Destroy(gameObject);
        }
    }
}
