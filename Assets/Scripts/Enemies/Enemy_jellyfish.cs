using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_jellyfish : MonoBehaviour
{

    Vector2 velocity;
    public float speed = 1;
    public float cooldown = 0.5f;
    float last;

    public Color color = Color.white;

    Rigidbody2D rb;
    Area area;

    void Start()
    {
        velocity = Vector2.zero;

        rb = GetComponent<Rigidbody2D>();
        area = GetComponentInChildren<Area>();

        GetComponent<SpriteRenderer>().material.color = color;
    }


    void FixedUpdate()
    {
        if (area.is_colliding)
        {
            Entity entity = area.getEntity();

            if (Time.time - last < cooldown)
            {
                return;
            }
            last = Time.time;
            if (entity != null && entity.team != GetComponent<Entity>().team) release();
        }
    }

    void release()
    {
        GameObject target = area.getObject();
        print(target.name);
        if (target != null)
        {
            if (target.transform.position.x < transform.position.x) velocity.x += speed;
            if (target.transform.position.x > transform.position.x) velocity.x -= speed;

            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }
}
