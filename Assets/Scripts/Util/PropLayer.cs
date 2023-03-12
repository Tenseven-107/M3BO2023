using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropLayer : MonoBehaviour
{
    public int prop_layer = -50;
    SpriteRenderer[] sprites;

    void Start()
    {
        sprites = GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer sprite in sprites) 
        {
            sprite.sortingOrder = prop_layer;
        }
    }
}
