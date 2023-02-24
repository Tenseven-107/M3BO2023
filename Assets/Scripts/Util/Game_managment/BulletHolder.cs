using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHolder : MonoBehaviour
{
    void Start()
    {
        if (gameObject.tag != "BulletHolder") gameObject.tag = "BulletHolder";
    }
}
