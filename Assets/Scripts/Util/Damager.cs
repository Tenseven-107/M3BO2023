using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    public int damage = 1;
    public int team = 0;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collider = collision.GetComponent<Entity>();
        if (collider)
        {
            if (collider.team != team)
            {
                collider.handleHit(damage);
            }
        }
    }
}
