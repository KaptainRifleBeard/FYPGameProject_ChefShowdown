using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public static void changeScene(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
