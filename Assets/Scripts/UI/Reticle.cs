using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    public float rotation_increase = 1;

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, (transform.eulerAngles.y + rotation_increase)));
    }
}
