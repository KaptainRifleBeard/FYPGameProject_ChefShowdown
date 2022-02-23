using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    //[SerializeField] private AudioSource stallsAudio;
    //[SerializeField] private AudioSource stallsAudio2;

    public AudioClip[] stallsClips;
    private AudioSource stallsAudio;

    public float soundDelay;

    //private AudioClip RandomClip
    //{
    //    get
    //    {
    //        return stallClips[(int)(Random.value * stallClips.Length)];
    //    }
    //}

    //IEnumerator PlayRandomSound()
    //{
    //    if(!stallClips)
    //}

    void Start()
    {
        stallsAudio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Player2")
        {
            //stallsAudio.Play();
            stallsAudio.clip = stallsClips[Random.Range(0, stallsClips.Length)];
            stallsAudio.PlayDelayed(soundDelay);
        }
    }

    void OnTriggerExit(Collider other)
    {
        stallsAudio.Stop();
        //stallsAudio.PlayDelayed(soundDelay);
    }


}
