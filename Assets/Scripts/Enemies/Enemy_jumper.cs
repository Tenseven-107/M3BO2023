using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy_jumper : MonoBehaviour
{
    public float jump_power = 50;
    bool grounded = true;

    Rigidbody2D rb;
    BulletShooter wp;

    void Start()
    {
        grounded = true;

        rb = GetComponent<Rigidbody2D>();
        wp = GetComponentInChildren<BulletShooter>();
    }


    void FixedUpdate()
    {
        RaycastHit2D ground_checker = Physics2D.Raycast(origin: new Vector2(rb.position.x, rb.position.y), direction: Vector2.down, distance: 1);
        if (ground_checker.collider != null && ground_checker.collider.tag == "Obstacle") grounded = true;

        RaycastHit2D hit = Physics2D.Raycast(origin: new Vector2(rb.position.x, rb.position.y), direction: Vector2.up, distance: Mathf.Infinity);
        if (hit.collider != null && hit.collider.gameObject.TryGetComponent<Entity>(out Entity entity) && grounded)
        {
            grounded = false;
            rb.AddForce(Vector2.up * jump_power);
            if (entity.team != GetComponent<Entity>().team) wp.fireCircle();
        }
    }
}
