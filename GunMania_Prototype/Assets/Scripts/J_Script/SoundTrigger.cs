using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource stallsAudio;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Player2")
        {
            stallsAudio.Play();
        }
    }

    void OnTriggerExit(Collider other)
    {
        stallsAudio.Stop();
    }


}
