using System;
using UnityEngine.Audio;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class sl_AudioManager : MonoBehaviour
{
    public sl_Sound[] sounds;
    public static sl_AudioManager instance;
    string audioName;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (sl_Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volumn;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

    }

    public void Play(string name)
    {
        sl_Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();

    }

    void Start()
    {
        //Play("BGM");
    }

    private void Update()
    {
        
    }

}
