using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip[] music_clips; // List of songs
    AudioSource music_source; // Audiosource of the player

    [Range( 0, 1)] public float volume = 1; // Volume of the music


    // Set up
    private void Awake()
    {
        music_source = gameObject.AddComponent<AudioSource>();
    }

    void Start()
    {
        music_source.loop = true;
        music_source.volume = volume;

        if (gameObject.tag != "MusicPlayer") gameObject.tag = "MusicPlayer";

        checkDoubles();
        setSong(0);
    }


    // Check if there's another musicplayer in the scene
    void checkDoubles()
    {
        GameObject[] music_players = GameObject.FindGameObjectsWithTag("MusicPlayer");

        if (music_players.Length > 1) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }



    // Set the song
    public void setSong(int song)
    {
        if (music_clips[song] != null && music_source.clip != music_clips[song]) music_source.clip = music_clips[song];
        else return;

        if (this.isActiveAndEnabled) music_source.Play();
    }
}
