using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class sl_MatchCountdown : MonoBehaviour
{
    public static float timeRemaining = 10; //5*60 = 300, rmb reset this in RematchAndLeave

    public bool timerIsRunning = false;
    public Text timeText;

    void Start()
    {
        if (PhotonNetwork.PlayerList.Length >= 2)
        {
            StartCoroutine(WaitToStart());
        }

    }


    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }

    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    IEnumerator WaitToStart()
    {
        yield return new WaitForSeconds(4.0f); //3 sec start game, 1 sec loading screen
        timerIsRunning = true;
    }
}
