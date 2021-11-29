using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer mainMixer;
    public AudioMixer sfxMixer;
    public void SetMainVolume (float volume)
    {
        mainMixer.SetFloat("Volume", volume);
    }

    public void SetSFXVolume (float volume)
    {
        sfxMixer.SetFloat("Volume", volume);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("sl_ServerLobby");
    }
}
