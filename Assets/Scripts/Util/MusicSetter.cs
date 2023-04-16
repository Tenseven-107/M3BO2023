using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSetter : MonoBehaviour
{
    public bool start_on_awake = true;
    public int song = 0;

    GameObject music_player;
    MusicPlayer mp;

    void Start()
    {
        music_player = GameObject.FindGameObjectWithTag("MusicPlayer");
        mp = music_player.GetComponent<MusicPlayer>();
        
        if (start_on_awake) mp.setSong(song);
    }


    public void play()
    {
        mp.setSong(song);
    }
}
