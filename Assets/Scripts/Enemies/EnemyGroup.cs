using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    private void OnTransformChildrenChanged()
    {
        if (transform.childCount <= 0) Destroy(gameObject);
    }
}
