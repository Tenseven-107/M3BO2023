using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastAttacker : MonoBehaviour
{
    public bool left = false;
    Vector2 dir;

    public BulletShooter wp;


    void Start()
    {
        if (left)
        {
            wp.transform.Rotate(0, 0, 0);
            dir = Vector2.right;
        }
        else
        {
            wp.transform.Rotate(0, 180, 0);
            dir = Vector2.left;
        }
    }

    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(origin: new Vector2(transform.position.x, transform.position.y), direction: dir, distance: Mathf.Infinity);
        if (hit.collider != null && hit.collider.gameObject.TryGetComponent<Entity>(out Entity entity))
        {
            if (entity.team != GetComponent<Entity>().team) wp.fire();
        }
    }
}
