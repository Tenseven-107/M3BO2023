using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScrolling : MonoBehaviour
{
    public float speed = 0.01f;

    SpriteRenderer sprite;
    GameObject child;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        child = transform.GetChild(0).gameObject;

        child.transform.position = new Vector2(-sprite.size.x, 0);
    }

    void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x + speed, 0);
        if (transform.position.x > sprite.size.x) transform.position = Vector2.zero;
    }
}
