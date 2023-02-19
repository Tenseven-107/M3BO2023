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
        if (collider)
        {
            if (collider.team != team)
            {
                collider.handleHit(damage);
                Destroy(gameObject);
            }
        }
        if (collision.tag == "Obstacle")
        {
            Destroy(gameObject);
        }
    }
}
