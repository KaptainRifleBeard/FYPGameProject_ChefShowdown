using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sl_StartScene : MonoBehaviour
{

    void Update()
    {
        if(Input.anyKey)
        {
            SceneManager.LoadScene("sl_ServerLobby");
        }
    }



    public void PressButtonStart() //for start scene
    {
        SceneManager.LoadScene("sl_ServerLobby");
    }
}
