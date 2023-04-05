using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSetter : MonoBehaviour
{
    public int song = 0;

    void Start()
    {
        GameObject music_player = GameObject.FindGameObjectWithTag("MusicPlayer");
        MusicPlayer mp = music_player.GetComponent<MusicPlayer>();
        
        mp.setSong(song);
    }
}
