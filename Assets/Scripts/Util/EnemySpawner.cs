using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public bool in_screen = false;

    public float time_out = 0.4f;
    public GameObject enemy;

    void Start()
    {
        in_screen = false;
        StartCoroutine(spawn());

        GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
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
            StartCoroutine(spawn());
        }
    }


    IEnumerator spawn()
    {
        float time = time_out;
        while (time > 0.1)
        {
            time -= 0.1f * Time.fixedDeltaTime;

            yield return null;
        }
        if (!in_screen) spawnEnemy();
        else yield break;
    }

    void spawnEnemy()
    {
        int children = transform.childCount + 1;

        if (children <= 1) Instantiate(enemy, transform);
    }


    private void OnApplicationQuit()
    {
        in_screen = false;
    }

    private void Update()
    {
        if (FindObjectOfType<SceneLoader>().loading) in_screen = false;
    }
}
