using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip[] music_clips;
    AudioSource music_source;


    void Start()
    {
        music_source = gameObject.AddComponent<AudioSource>();
        music_source.loop = true;

        if (gameObject.tag != "MusicPlayer") gameObject.tag = "MusicPlayer";

        checkDoubles();
        setSong(0);
    }


    void checkDoubles()
    {
        GameObject[] music_players = GameObject.FindGameObjectsWithTag("MusicPlayer");

        if (music_players.Length > 1) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }


    public void setSong(int song)
    {
        if (music_clips[song] != null && music_source.clip != music_clips[song]) music_source.clip = music_clips[song];
        else return;

        music_source.Play();
    }
}
