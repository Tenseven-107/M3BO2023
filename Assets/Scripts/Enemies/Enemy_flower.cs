using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_flower : MonoBehaviour
{
    BulletShooter wp;

    void Start()
    {
        wp = GetComponentInChildren<BulletShooter>();

        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }


    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(origin: new Vector2(transform.position.x, transform.position.y), direction: Vector2.up, distance: Mathf.Infinity);
        if (hit.collider != null && hit.collider.gameObject.TryGetComponent<Entity>(out Entity entity))
        {
            if (entity.team != GetComponent<Entity>().team) wp.fire();
        }
    }
}

