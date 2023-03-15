using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    public string collider_name = "Player";
    public bool stay = false;
    public bool is_colliding = false;

    new GameObject collider;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (stay && collision.name == collider_name)
        {
            collider = collision.gameObject;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == collider_name)
        {
            collider = collision.gameObject;
            is_colliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == collider_name)
        {
            collider = null;
            is_colliding = false;
        }
    }


    public Entity getEntity()
    {
        if (collider != null) return collider.GetComponent<Entity>();
        else return null;
    }

    public GameObject getObject()
    {
        if (collider != null) return collider;
        else return null;
    }
}
