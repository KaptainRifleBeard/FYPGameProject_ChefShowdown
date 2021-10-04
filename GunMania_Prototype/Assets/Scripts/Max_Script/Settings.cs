using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Canvas settingsPanel;
    public Slider mainvol, sfxvol, musicvol;
    public bool isActive;




    public void mainVol(float val)
    {
        PlayerPrefs.SetFloat("MainVolume", val);
    }

    public void sfxVol(float val)
    {

        PlayerPrefs.SetFloat("SFXVolume", val);
    }
    public void musicVol(float val)
    {

        PlayerPrefs.SetFloat("MusicVolume", val);
    }

    void Awake()
    {
        mainvol.value = PlayerPrefs.GetFloat("MainVolume");
        sfxvol.value = PlayerPrefs.GetFloat("SFXVolume");
        musicvol.value = PlayerPrefs.GetFloat("MusicVolume");
    }

    public void startSettingsPanel()
    {
        settingsPanel.gameObject.SetActive(!settingsPanel.isActiveAndEnabled);
    }
}
