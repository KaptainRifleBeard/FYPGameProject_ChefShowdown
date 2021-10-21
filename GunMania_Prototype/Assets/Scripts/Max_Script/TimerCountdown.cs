using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using TMPro;


public class TimerCountdown : MonoBehaviour
{
    public Text timeText;
    public sl_WinLoseUI winLose;
    public float timeValue = 90;


    [Header("Debugging WINLOSEUISTUFF")]
    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject exitScreen;


    // Update is called once per frame
    void Update()
    {
        if (timeValue > 0)
        {
            timeValue -= Time.deltaTime;
        }
        else if (timeValue < 0)
        {
            timeValue = 0;
            CheckWinOrLose();


        }
        else if (sl_PlayerHealth.currentHealth == 0 || sl_P2PlayerHealth.p2currentHealth == 0)
        {
            CheckWinOrLose();
        }
        // Debugging Code ONLY USED TO SUICIDE P1.
        else if (Input.GetKeyDown(KeyCode.F6))
        {
            if (PhotonNetwork.IsMasterClient)
            {

                sl_PlayerHealth.currentHealth = 0;
            }
            else
            {
                sl_P2PlayerHealth.p2currentHealth = 0;
            }
        }

        DisplayTime(timeValue);
    }


    void CheckWinOrLose()
    {
        if (sl_PlayerHealth.currentHealth < sl_P2PlayerHealth.p2currentHealth)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                //winLose.text.text = "Click to leave room";
                //winLose.loseScreen.SetActive(true);
                loseScreen.SetActive(true);
            }
            else
            {
                //winLose.winScreen.SetActive(true);
                winScreen.SetActive(true);
            }
        }

        else if (sl_P2PlayerHealth.p2currentHealth > sl_PlayerHealth.currentHealth)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                //winLose.text.text = "Click to leave room";
                //winLose.winScreen.SetActive(true);
                winScreen.SetActive(true);
            }
            else
            {
                //winLose.loseScreen.SetActive(true);
                loseScreen.SetActive(true);
            }
        }   
        else if (sl_P2PlayerHealth.p2currentHealth == sl_PlayerHealth.currentHealth)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                //winLose.text.text = "Click to leave room";
                //winLose.winScreen.SetActive(true);
                winScreen.SetActive(true);
            }
            else
            {
                //winLose.winScreen.SetActive(true);
                winScreen.SetActive(true);
            }
            // in the even that there is a tie game, both sides win.
        }



    }

    void DisplayTime(float timetoDisplay)
    {
        float minutes;
        float seconds;
        if (timetoDisplay < 0 )
        {
            timetoDisplay = 0;
            //snaps time back to 0 instead of negs
        }
        else if (timetoDisplay > 0)
        {
            timetoDisplay += 1;
            // to compensate when timeValue is still within seconds - milliseconds.
        }


        minutes = Mathf.FloorToInt(timetoDisplay / 60);
        //whole numbers
        seconds = Mathf.FloorToInt(timetoDisplay % 60);
        // remainder

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        // time string value Formatting , 1st arg, 2nd arg, 1st val, 2nd val.
    }
}
