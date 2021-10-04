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

    public void Awake()
    {
        PlayerPrefs.SetInt("Character3", -1);
        PlayerPrefs.SetInt("Character4", -1);
    }

    public static void quitGame()
    {
        Application.Quit();
    }
}
