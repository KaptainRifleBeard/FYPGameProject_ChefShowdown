using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    public AudioSource stallSFX;

    void OnTriggerEnter(Collider other)
    {
        stallSFX.Play();
    }


}
