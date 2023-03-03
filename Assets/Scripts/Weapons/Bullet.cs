using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10;
    public int damage = 1;
    public int team = 0;
    public bool piercing = false;
    public Color color = Color.white;
    Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;

        GetComponent<SpriteRenderer>().material.color = color;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collider = collision.GetComponent<Entity>();
        if (collision.GetComponent<Shield>() != null) collideShield(collision);

        if (collider)
        {
            if (collider.team != team)
            {
                collider.handleHit(damage);

                if (!piercing) Destroy(gameObject);
            }
        }
        if (collision.tag == "Obstacle")
        {
            Destroy(gameObject);
        }
    }

    private void collideShield(Collider2D collision)
    {
        Shield collider = collision.GetComponent<Shield>();
        if (collider)
        {
            if (collider.team != team)
            {
                collider.handleHit(damage);
                Destroy(gameObject);
            }
        }
    }
}
