using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game has been quit");
    }

    public void RetryGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
