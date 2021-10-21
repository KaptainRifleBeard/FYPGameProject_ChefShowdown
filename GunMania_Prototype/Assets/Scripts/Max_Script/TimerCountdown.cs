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

    // Update is called once per frame
    void Update()
    {
        if (timeValue >0)
        {
            timeValue -= Time.deltaTime;
        }
        else
        {
            timeValue = 0;
            CheckWinOrLose();


        }

        DisplayTime(timeValue);
    }


    void CheckWinOrLose()
    {
        if (sl_PlayerHealth.currentHealth > sl_P2PlayerHealth.p2currentHealth)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                //winLose.text.text = "Click to leave room";
                winLose.loseScreen.SetActive(true);
            }
            else
            {
                winLose.winScreen.SetActive(true);
            }
        }

        if (sl_P2PlayerHealth.p2currentHealth < sl_PlayerHealth.currentHealth)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                //winLose.text.text = "Click to leave room";
                winLose.winScreen.SetActive(true);
            }
            else
            {
                winLose.loseScreen.SetActive(true);
            }
        }   
        if (sl_P2PlayerHealth.p2currentHealth == sl_PlayerHealth.currentHealth)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                //winLose.text.text = "Click to leave room";
                winLose.winScreen.SetActive(true);
            }
            else
            {
                winLose.winScreen.SetActive(true);
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