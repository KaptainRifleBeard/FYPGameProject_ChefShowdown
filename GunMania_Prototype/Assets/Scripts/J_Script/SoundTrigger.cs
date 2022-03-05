using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    //public AudioClip[] stallsClips;
    public AudioSource[] stallsAudio;

    //public float soundDelay;

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
    float num;

    void Start()
    {
        //stallsAudio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Player2")
        {
            num = Random.Range(0, stallsAudio.Length);

            if(num == 0)
            {
                stallsAudio[0].Play();
                stallsAudio[0].volume = 0.5f;

            }
            if (num == 1)
            {
                stallsAudio[1].Play();
                stallsAudio[1].volume = 0.5f;

            }
            if (num == 2)
            {
                stallsAudio[2].Play();
                stallsAudio[2].volume = 0.5f;

            }

            //stallsAudio.clip = stallsClips[Random.Range(0, stallsClips.Length)];
            //stallsAudio.volume += 0.2f;

            //stallsAudio.PlayDelayed(soundDelay);
        }
    }

    void OnTriggerExit(Collider other)
    {
        StartCoroutine(DecreaseVolumn());
        //stallsAudio.Stop();
        //stallsAudio.PlayDelayed(soundDelay);
    }

    IEnumerator DecreaseVolumn()
    {
        if (num == 0)
        {
            for (int i = 0; i < 8; i++)
            {
                yield return new WaitForSeconds(0.1f);
                stallsAudio[0].volume -= 0.05f;

            }

            if (stallsAudio[0].volume <= 0.1)
            {
                stallsAudio[0].Stop();
            }
        }

        if (num == 1)
        {
            for (int i = 0; i < 8; i++)
            {
                yield return new WaitForSeconds(0.1f);
                stallsAudio[1].volume -= 0.05f;

            }
            if (stallsAudio[1].volume <= 0.1)
            {
                stallsAudio[1].Stop();
            }
        }

        if (num == 2)
        {
            for (int i = 0; i < 8; i++)
            {
                yield return new WaitForSeconds(0.1f);
                stallsAudio[2].volume -= 0.05f;

            }

            if (stallsAudio[2].volume <= 0.1)
            {
                stallsAudio[2].Stop();
            }
        }

    }


}
