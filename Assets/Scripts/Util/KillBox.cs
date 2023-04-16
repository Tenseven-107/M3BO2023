using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour
{
    public bool active_awake = true;

    private bool active = false;
    public bool Active
    {
        get
        {
            return active;
        } 
        set
        {
            active = value;
            gameObject.SetActive(value);
        }
    }

    public int team = 1;



    private void Start()
    {
        if (active_awake) Active = true;
        else Active = false;
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        Entity entity = obj.GetComponent<Entity>();

        if (entity != null && obj.activeSelf)
        {
            if (entity.team != team && Active)
            {
                entity.die();
            }
        }
    }
}
