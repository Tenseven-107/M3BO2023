using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CombatDroneLogic : MonoBehaviour
{
    public float speed = 25;
    public float amplitude = 1.5f;
    public float frequency = 2;

    BossCenter center;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        center = GetComponentInChildren<BossCenter>();
    }


    private void FixedUpdate()
    {
        Vector3 relative = transform.InverseTransformPoint(center.pos);
        float angle = (Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg) + 90;
        transform.Rotate(0, 0, -angle);

        release();
    }

    void release()
    {
        Vector2 pos = rb.velocity;

        float sine = Mathf.Sin(pos.x * frequency) * amplitude;
        pos = new Vector2(transform.up.x + sine, transform.up.y + sine);

        rb.velocity = pos * speed * Time.fixedDeltaTime;
    }
}
