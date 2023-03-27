using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    Vector3 cam_offset = new Vector3(0,0,-10);
    public float smoothing = 25;

    public float X_confiner = 0;
    public float Y_confiner = 0;

    public GameObject target;

    float hitstop_time = 0;

    float screenshake_time = 0;
    float screenshake_intensity = 0;
    public float screenshake_mod = 1;


    void Start()
    {
        Time.timeScale = 1;

        if (target != null) transform.position = target.transform.position + cam_offset;
        if (gameObject.tag != "MainCamera") gameObject.tag = "MainCamera";

        string path = Path.Combine(Application.persistentDataPath, "SettingsSave.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SettingsSave save = JsonUtility.FromJson<SettingsSave>(json);
            screenshake_mod = save.screenshake;
        }
    }


    void FixedUpdate()
    {
        if (target != null)
        {
            setCamPos();
        }
    }


    void setCamPos()
    {
        Vector3 current_cam_pos = transform.position;
        Vector3 new_cam_pos = target.transform.position + cam_offset;
        Vector3 pos = Vector3.Lerp(current_cam_pos, new_cam_pos, smoothing * Time.fixedDeltaTime);

        if (X_confiner != 0 && Y_confiner != 0)
        {
            pos.x = Mathf.Clamp(pos.x, -X_confiner, X_confiner);
            pos.y = Mathf.Clamp(pos.y, -Y_confiner, Y_confiner);
        }

        transform.position = pos;
    }


    public void hitstop(float hitstop_time)
    { 
        this.hitstop_time = hitstop_time;
        StartCoroutine(hitstopLoop());
    }

    IEnumerator hitstopLoop()
    {
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(hitstop_time);
        Time.timeScale = 1;
        yield break;
    }


    public void screenshake(float screenshake_time, float screenshake_intensity)
    {
        if (target != null && !checkIfBordered()) transform.position = target.transform.position + cam_offset;

        this.screenshake_time = screenshake_time;
        this.screenshake_intensity = screenshake_intensity / screenshake_mod;
        if (screenshake_intensity != 0 && !(screenshake_mod <= -100) && !checkIfBordered()) StartCoroutine(screenshakeLoop());
    }

    IEnumerator screenshakeLoop() 
    {
        Vector3 original_pos = new Vector3(transform.position.x, transform.position.y, cam_offset.z);

        for (float n = 0; n < screenshake_time; n += 0.01f)
        {
            float X = Random.Range(-screenshake_intensity, screenshake_intensity);
            float Y = Random.Range(-screenshake_intensity, screenshake_intensity);
            Vector3 shake = original_pos + new Vector3(X, Y);

            transform.position = Vector3.Lerp(original_pos, shake, 1);

            yield return null;
        }
        transform.position = original_pos;
        yield break;
    }


    bool checkIfBordered()
    {
        float x = transform.position.x;
        float y = transform.position.y;

        if (x >= X_confiner || x <= -X_confiner || y >= Y_confiner || y <= -Y_confiner)
        {
            return true;
        }
        else return false;
    }
}
