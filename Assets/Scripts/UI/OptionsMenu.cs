using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public GameObject main_menu;
    const int default_framerate = 60;

    public Toggle fullscreen;
    public Slider framerate;
    public Slider volume;


    void Start()
    {
        gameObject.SetActive(false);
        Application.targetFrameRate = default_framerate;

        fullscreen.isOn = Screen.fullScreen;
        framerate.value = Application.targetFrameRate;
        // volume.value = idk;

        volume.onValueChanged.AddListener((volume) =>
        {
            print(volume);
        });
        framerate.onValueChanged.AddListener((framerate) =>
        {
            Application.targetFrameRate = (int)framerate;
        });
        fullscreen.onValueChanged.AddListener((value) =>
        {
            Screen.fullScreen = value;
        });
    }

    public void Back()
    {
        gameObject.SetActive(false);
        main_menu.SetActive(true);
    }
}
