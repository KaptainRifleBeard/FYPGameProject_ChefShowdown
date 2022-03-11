using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        guideScreen.SetActive(false);

        control.SetActive(true);
        dish.SetActive(false);
        buttonnext_dish.SetActive(false);

        button_control.SetActive(false);
        button_dish.SetActive(true);
    }


    void Update()
    {
        
    }

    public void ExitGuideScreen()
    {
        guideScreen.SetActive(false);
    }

    public void OpenGuideScreen()
    {
        guideScreen.SetActive(true);
    }

    public void OpenDishGuide()
    {
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
