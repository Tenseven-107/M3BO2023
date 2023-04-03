using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudio : MonoBehaviour
{
    public AudioClip[] clips; // Audio clips
    AudioSource audio;

    [Range(0.01f, 3)] public float max_pitch = 1; // Maximum or minimum pitch of the sound (Leave at 1 for no randomized pitch)
    [Range(0, 1)] public float min_volume = 1; // Minimum volume of the sound (Leave at 1 for no randomized volume)

    int clips_number;

    public bool play_on_awake = false; // Play when object is created


    // Set up
    private void Start()
    {
        audio = gameObject.AddComponent<AudioSource>(); // Create new audiosource
        clips_number = clips.Length;

        if (play_on_awake) playSound();
    }


    // Play a random sound
    public void playSound()
    {
        AudioClip play_clip = clips[Random.Range(0, clips_number)];
        audio.clip = play_clip;

        float pitch = Random.Range(1, max_pitch); // Pick random number of pitch
        float volume = Random.Range(min_volume, 1); // Pick random number of volume

        audio.pitch = pitch;
        audio.volume = volume;

        audio.Play();
    }
}
