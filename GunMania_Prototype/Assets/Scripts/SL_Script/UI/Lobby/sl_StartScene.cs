using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sl_StartScene : MonoBehaviour
{
    private void Update()
    {
        if(Input.anyKey)
        {
            SceneManager.LoadScene("sl_SetNameMenu");
        }
    }


    public void ToSetName()
    {
        SceneManager.LoadScene("sl_SetNameMenu");
    }
}
