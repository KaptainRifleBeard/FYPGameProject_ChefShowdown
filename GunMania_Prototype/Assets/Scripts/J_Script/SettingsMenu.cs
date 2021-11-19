using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer mainMixer;
    public AudioMixer sfxMixer;
    public void SetVolume (float volume)
    {
        mainMixer.SetFloat("Volume", volume);
        sfxMixer.SetFloat("Volume", volume);
    }
}
