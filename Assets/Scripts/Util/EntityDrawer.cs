using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDrawer : MonoBehaviour
{
    public float despawn_time = 7;
    public bool in_screen = false;


    private void OnBecameVisible()
    {
        if (!in_screen)
        {
            in_screen = true;
        }
    }


    private void OnBecameInvisible()
    {
        if (in_screen)
        {
            in_screen = false;
            StartCoroutine(WaitAndDespawn());
        }
    }


    IEnumerator WaitAndDespawn()
    {
        float time = despawn_time;
        while (time > 0.1 && !in_screen)
        {
            time -= 0.1f * Time.fixedDeltaTime;

            yield return null;
        }
        if (!in_screen) Destroy(gameObject);
        else yield break;
    }


    private void OnApplicationQuit()
    {
        in_screen = false;
    }
}
