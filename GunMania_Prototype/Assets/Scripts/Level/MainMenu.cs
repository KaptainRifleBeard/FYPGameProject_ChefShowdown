using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void LevelSelect()
    {
        SceneManager.LoadScene("Level01");
    }
    public void mainMenu()
    {
        SceneManager.LoadScene("CharacterSelect");
    }
    public void CharacterSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
