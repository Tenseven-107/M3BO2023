using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : MonoBehaviour
{
    Vector2 velocity;
    public float speed = 1;
    public bool left = false;

    Vector2 dir;

    Rigidbody2D rb;
    BulletShooter wp;


    void Start()
    {
        velocity = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();
        wp = GetComponentInChildren<BulletShooter>();

        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

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


    // Update is called once per frame
    void FixedUpdate()
    {
        if (left) velocity.x += speed * Time.fixedDeltaTime;
        else velocity.x -= speed * Time.fixedDeltaTime;

        velocity.x = Mathf.Clamp(velocity.x, -speed, speed);
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

        RaycastHit2D hit = Physics2D.Raycast(origin: new Vector2(rb.position.x, rb.position.y), direction: dir, distance: Mathf.Infinity);
        if (hit.collider != null && hit.collider.gameObject.TryGetComponent<Entity>(out Entity entity))
        {
            if (entity.team != GetComponent<Entity>().team) wp.fire();
        }
    }
}
