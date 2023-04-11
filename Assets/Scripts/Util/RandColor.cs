using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandColor : MonoBehaviour
{
    void Start()
    {
        GetComponent<SpriteRenderer>().color = Random.ColorHSV(0, 1, 1, 1, 1, 1);
    }
}
