using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public GameObject main_menu;

    const bool default_fullscreen = false;
    const float default_screenshake = 1;
    const int default_framerate = 60;
    const float default_volume = 1;

    bool fullscreen_bool;
    float screenshake_value;
    int framerate_value;
    float volume_value;

    public Toggle fullscreen;
    public Slider screenshake;
    public TextMeshProUGUI framerate_text;
    public Slider framerate;
    public Slider volume;

    string path;


    void Start()
    {
        gameObject.SetActive(false);

        path = Path.Combine(Application.persistentDataPath, "SettingsSave.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SettingsSave save = JsonUtility.FromJson<SettingsSave>(json);

            setSettings(save);
        }
        else
        {
            setDefaultSettings();
            applySettings();
        }

        Screen.fullScreen = fullscreen_bool;
        Application.targetFrameRate = framerate_value;
        AudioListener.volume = volume_value;

        fullscreen.onValueChanged.AddListener((value) =>
        {
            fullscreen_bool = value;
            Screen.fullScreen = value;
            applySettings();
        });
        screenshake.onValueChanged.AddListener((screenshake_mod) =>
        {
            screenshake_value = screenshake_mod;
            if (screenshake_value < 1 && screenshake_value > -1) screenshake_value = -2;

            applySettings();
        });
        framerate.onValueChanged.AddListener((framerate) =>
        {
            framerate_value = (int)framerate;
            Application.targetFrameRate = framerate_value;

            framerate_text.text = "framerate: " + framerate_value.ToString();

            applySettings();
        });
        volume.onValueChanged.AddListener((volume) =>
        {
            volume_value = volume;
            AudioListener.volume = volume;
            applySettings();
        });
    }

    public void Back()
    {
        gameObject.SetActive(false);
        main_menu.SetActive(true);
    }

    void applySettings()
    {
        SettingsSave save = new SettingsSave();

        save.fullscreen = fullscreen_bool;
        save.screenshake = screenshake_value;
        save.framerate = framerate_value;
        save.volume = volume_value;

        string json = JsonUtility.ToJson(save);

        File.WriteAllText(path, json);
    }


    void setSettings(SettingsSave save)
    {
        fullscreen_bool = save.fullscreen;
        screenshake_value = save.screenshake;
        framerate_value = save.framerate;
        volume_value = save.volume;

        fullscreen.isOn = fullscreen_bool;
        screenshake.value = screenshake_value;
        framerate_text.text = "framerate: " + framerate_value.ToString();
        framerate.value = framerate_value;
        volume.value = volume_value;
    }

    void setDefaultSettings()
    {
        fullscreen_bool = default_fullscreen;
        screenshake_value = default_screenshake;
        framerate_value = default_framerate;
        volume_value = default_volume;

        fullscreen.isOn = fullscreen_bool;
        screenshake.value = screenshake_value;
        framerate_text.text = "framerate: " + framerate_value.ToString();
        framerate.value = framerate_value;
        volume.value = default_volume;
    }
}
