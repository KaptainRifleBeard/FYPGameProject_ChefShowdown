using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioContinuation : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
