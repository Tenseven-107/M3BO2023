using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_3 : MonoBehaviour
{
    Vector2 velocity;
    public float speed = 1;
    public bool left = false;

    Rigidbody2D rb;
    BulletShooter wp;
    Area area;


    void Start()
    {
        velocity = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();
        wp = GetComponentInChildren<BulletShooter>();
        area = GetComponentInChildren<Area>();

        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }


    void FixedUpdate()
    {
        if (left) velocity.x += speed * Time.fixedDeltaTime;
        else velocity.x -= speed * Time.fixedDeltaTime;

        velocity.x = Mathf.Clamp(velocity.x, -speed, speed);
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

        if (area.is_colliding)
        {
            Entity entity = area.getEntity();
            if (entity != null && entity.team != GetComponent<Entity>().team) wp.fireCircle();
        }
    }
}
