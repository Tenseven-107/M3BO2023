using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy_jellyfish : MonoBehaviour
{
    public float speed = 25;
    public float amplitude = 1.5f;
    public float frequency = 2;

    Rigidbody2D rb;
    Area area;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        area = GetComponentInChildren<Area>();

        GetComponent<SpriteRenderer>().color = Random.ColorHSV(0, 1, 1, 1, 1, 1);
    }


    void FixedUpdate()
    {
        GameObject target = area.getObject();

        if (area.is_colliding && target != null)
        {
            Vector2 relative = transform.InverseTransformPoint(target.transform.position);
            float angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
            transform.Rotate(0, 0, -angle);

            release();
        }
        else rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, 0.1f);
    }

    void release()
    {
        Vector2 pos = rb.velocity;

        float sine = Mathf.Sin(pos.x * frequency) * amplitude;
        pos = new Vector2(transform.up.x + sine, transform.up.y + sine);

        rb.velocity = pos * speed * Time.fixedDeltaTime;
    }
}
