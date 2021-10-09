using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
    public static void changeScene(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public static void quitGame()
    {
        Application.Quit();
    }
}
