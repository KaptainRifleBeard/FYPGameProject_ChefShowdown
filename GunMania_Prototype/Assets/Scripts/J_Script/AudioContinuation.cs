using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioContinuation : MonoBehaviour
{

    public AudioSource audio;
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    private void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        if(sceneName != "sl_TestScene")
        {
            audio.volume = 0.5f;
        }
        else
        {
            audio.volume = 0f;
        }
    }
}
