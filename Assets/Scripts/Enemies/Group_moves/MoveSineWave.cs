using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSineWave : MonoBehaviour
{
    public float amplitude = 1;
    public float frequency = 2;
    public bool inverted = false;

    float normal_y;

    void Start()
    {
        normal_y = transform.position.y;
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        float sine = Mathf.Sin(pos.x * frequency) * amplitude;
        if (inverted) sine *= -1;

        pos.y = normal_y + sine;

        transform.position = pos;
    }
}
