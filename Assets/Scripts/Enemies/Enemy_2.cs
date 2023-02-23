using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : MonoBehaviour
{
    Vector2 velocity;
    public float speed = 1;
    public bool right = false;

    Rigidbody2D rb;
    BulletShooter wp;

    void Start()
    {
        velocity = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();
        wp = GetComponentInChildren<BulletShooter>();

        if (right) wp.transform.Rotate(0, 0, 0);
        else wp.transform.Rotate(0, 180, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (right) velocity.x += speed * Time.fixedDeltaTime;
        else velocity.x -= speed * Time.fixedDeltaTime;

        velocity.x = Mathf.Clamp(velocity.x, -speed, speed);
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

        RaycastHit2D hit = Physics2D.Raycast(origin: new Vector2(rb.position.x, rb.position.y), direction: Vector2.left, distance: Mathf.Infinity);
        if (hit.collider != null)
        {
            Entity entity = hit.collider.gameObject.GetComponent<Entity>();
            if (entity.team != GetComponent<Entity>().team) wp.fire();
        }
    }
}
