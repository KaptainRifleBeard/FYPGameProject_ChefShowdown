using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sl_GuideScreen : MonoBehaviour
{
    public GameObject guideScreen;

    public GameObject dish;
    public GameObject control;

    public GameObject[] mouseOrContrller;
    public GameObject[] dishes;

    public int controlNum;
    public int dishNum;

    public GameObject buttonnext_dish;
    public GameObject button_dish;
    public GameObject button_control;

    string audioName;
    public AudioSource[] audio;
    int sfxNum;

    void Start()
    {
        //guideScreen.SetActive(false);

        control.SetActive(true);
        dish.SetActive(false);
        buttonnext_dish.SetActive(false);

        button_control.SetActive(false);
        button_dish.SetActive(true);
    }


    void Update()
    {
       
    }

    public void PlaySound()
    {
        if (dishNum == 0 || dishNum == 1)
        {
            audio[0].Play();
        }
        if (dishNum == 2 || dishNum == 3)
        {
            audio[1].Play();

        }
        if (dishNum == 4 || dishNum == 5)
        {
            audio[2].Play();

        }
        if (dishNum == 6 || dishNum == 7)
        {
            audio[3].Play();

        }
    }

    public void ToGuideScreen()
    {
        SceneManager.LoadScene("GuideScreen");
    }

    public void ToSLGuideScreen()
    {
        SceneManager.LoadScene("sl_GuideScreen");
    }

    public void BackToServerLobby()
    {
        SceneManager.LoadScene("sl_ServerLobby");
    }

    public void BackToSetNameMenu()
    {
        SceneManager.LoadScene("sl_SetNameMenu");
    }

    public void OpenDishGuide()
    {
        PlaySound();

        dish.SetActive(true);
        control.SetActive(false);

        buttonnext_dish.SetActive(true);

        button_dish.SetActive(false);
        button_control.SetActive(true);


    }

    public void OpenControlGuide()
    {
        dish.SetActive(false);
        control.SetActive(true);

        buttonnext_dish.SetActive(false);

        button_dish.SetActive(true);
        button_control.SetActive(false);
    }

    public void NextButton()
    {
        mouseOrContrller[controlNum].SetActive(false);
        controlNum = (controlNum + 1) % mouseOrContrller.Length;
        mouseOrContrller[controlNum].SetActive(true);

    }

    public void NextButton_dish()
    {
        dishes[dishNum].SetActive(false);
        dishNum = (dishNum + 1) % dishes.Length;
        dishes[dishNum].SetActive(true);

        audio[sfxNum].Stop();
        sfxNum = (sfxNum + 1) % audio.Length;
        audio[sfxNum].Play();
    }

    public void PrevButton()
    {
        mouseOrContrller[controlNum].SetActive(false);
        controlNum--;

        if (controlNum < 0)
        {
            controlNum += mouseOrContrller.Length;
        }
        mouseOrContrller[controlNum].SetActive(true);

    }

}
