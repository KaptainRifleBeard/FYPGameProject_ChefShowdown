using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sl_GuideScreenSFX : MonoBehaviour
{
    public AudioSource audio;

    void Start()
    {
        
    }

    void Update()
    {
        if(gameObject.activeInHierarchy)
        {
            audio.Play();
        }
        else
        {
            audio.Stop();
        }
    }
}
