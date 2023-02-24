using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntityDrawer : MonoBehaviour
{
    public float despawn_time = 0.5f;
    float long_despawn_time;
    public bool in_screen = false;


    private void Start()
    {
        in_screen = false;
        long_despawn_time = despawn_time * 5;
        StartCoroutine(despawn());
    }


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

    IEnumerator despawn()
    {
        float time = long_despawn_time;
        while (time > 0.1)
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

    private void Update()
    {
        if (FindObjectOfType<SceneLoader>().loading) in_screen= false;
    }
}
