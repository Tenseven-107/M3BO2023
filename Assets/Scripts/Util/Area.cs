using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    public bool is_colliding = false;
    GameObject collider;

    private void OnTriggerStay2D(Collider2D collision)
    {
        collider = collision.gameObject;
        is_colliding = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        is_colliding = false;
    }


    public Entity getEntity()
    {
        if (collider != null) return collider.GetComponent<Entity>();
        else return null;
    }

    public GameObject getObject()
    {
        if (collider != null) return collider.GetComponent<GameObject>();
        else return null;
    }
}
