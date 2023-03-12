using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    Vector3 cam_offset = new Vector3(0,0,-10);
    public float smoothing = 25;

    public float X_confiner = 0;
    public float Y_confiner = 0;

    public Player player;

    void Start()
    {
        transform.position = player.transform.position + cam_offset;
        if (gameObject.tag != "MainCamera") gameObject.tag = "MainCamera";
    }

    
    void FixedUpdate()
    {
        if (player != null)
        {
            setCamPos();
        }
    }


    void setCamPos()
    {
        Vector3 current_cam_pos = transform.position;
        Vector3 new_cam_pos = player.transform.position + cam_offset;
        Vector3 pos = Vector3.Lerp(current_cam_pos, new_cam_pos, smoothing * Time.fixedDeltaTime);

        if (X_confiner != 0 && Y_confiner != 0)
        {
            pos.x = Mathf.Clamp(pos.x, -X_confiner, X_confiner);
            pos.y = Mathf.Clamp(pos.y, -Y_confiner, Y_confiner);
        }

        transform.position = pos;
    }
}
