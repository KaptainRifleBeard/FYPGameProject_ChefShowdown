using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject openNicknameUI;
    public GameObject creditScreen;

    private void Start()
    {
        openNicknameUI.SetActive(false);
        creditScreen.SetActive(false);
        settingsMenu.SetActive(false);
    }

    public void StartGame()
    {
        //SceneManager.LoadScene("sl_ServerLobby");
    }

    public void OpenSettingsMenu()
    {
        if (settingsMenu != null)
        {
            bool isActive = settingsMenu.activeSelf;
            settingsMenu.SetActive(!isActive);
        }
    }

    public void CloseSettingsMenu()
    {
        settingsMenu.SetActive(false);

    }

    public void HideNicknameUI()
    {
        openNicknameUI.SetActive(false);

    }

    public void OpenCreditScreen()
    {
        creditScreen.SetActive(true);

    }
    public void CloseCreditScreen()
    {
        creditScreen.SetActive(false);

    }

    public void ToServerLobby()
    {
        SceneManager.LoadScene("sl_ServerLobby");
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
