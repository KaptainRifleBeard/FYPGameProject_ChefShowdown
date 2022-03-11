using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class sl_MatchCountdown : MonoBehaviour
{
    public static float timeRemaining = 300; //5*60 = 300, rmb reset this in RematchAndLeave's rematch and start()

    public bool timerIsRunning = false;
    public Text timeText;

    PhotonView view;
    void Start()
    {
        view = GetComponent<PhotonView>();

        if (PhotonNetwork.PlayerList.Length >= 2)
        {
            StartCoroutine(WaitToStart());
        }

    }


    void Update()
    {
        if (timerIsRunning)
        {
            TimeRunning();
        }

    }

    [PunRPC]
    public void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void TimeRunning()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            //DisplayTime(timeRemaining);
            view.RPC("DisplayTime", RpcTarget.All, timeRemaining);

        }
        else
        {
            timeRemaining = 0;
            timerIsRunning = false;

            view.RPC("DisplayTime", RpcTarget.All, timeRemaining);
        }
    }

    IEnumerator WaitToStart()
    {
        yield return new WaitForSeconds(4.0f); //3 sec start game, 1 sec loading screen
        timerIsRunning = true;
    }
}
