using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using UnityEngine.SceneManagement;
using TMPro;

public class sl_WinLoseUI : MonoBehaviourPunCallbacks
{
    public TMP_Text text;

    public static readonly byte RestartEventCode = 1;

    public GameObject winScreen;
    public GameObject loseScreen;
    void Start()
    {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
    }



    void Update()
    {
        StartCoroutine(WaitStartGame());

        //if(sl_PlayerHealth.currentHealth == 0)
        //{
        //    if (PhotonNetwork.IsMasterClient)
        //    {
        //        StartCoroutine(DisplayMessage("Red Win"));
        //        StartCoroutine(RestartGame());
        //    }
        //    else
        //    {
        //        StartCoroutine(DisplayMessage("Blue Lose"));
        //    }
        //}

        //if (sl_P2PlayerHealth.p2currentHealth == 0)
        //{
        //    if (PhotonNetwork.IsMasterClient)
        //    {
        //        StartCoroutine(DisplayMessage("Red Win"));
        //        StartCoroutine(RestartGame());
        //    }
        //    else
        //    {
        //        StartCoroutine(DisplayMessage("Blue Lose"));
        //    }
        //}

    }

    //IEnumerator RestartGame()
    //{
    //    sl_PlayerHealth.currentHealth = 8;
    //    sl_P2PlayerHealth.p2currentHealth = 8;
    //    yield return new WaitForSeconds(1.0f);

    //    RaiseEventOptions eventOpt = new RaiseEventOptions { Receivers = ReceiverGroup.All };
    //    ExitGames.Client.Photon.SendOptions sendOptions = new ExitGames.Client.Photon.SendOptions { Reliability = true };
    //    PhotonNetwork.RaiseEvent(RestartEventCode, null, eventOpt, sendOptions);
    //}

    IEnumerator WaitStartGame()
    {
        yield return new WaitForSeconds(1.0f);
        WinLoseCondition();
    }

    void WinLoseCondition()
    {

        if (sl_PlayerHealth.currentHealth == 0)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                text.text = "Click to leave room";
                loseScreen.SetActive(true);
            }
            else
            {
                winScreen.SetActive(true);
            }
        }

        if (sl_P2PlayerHealth.p2currentHealth == 0)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                text.text = "Click to leave room";
                winScreen.SetActive(true);
            }
            else
            {
                loseScreen.SetActive(true);
            }
        }
    }

    public void QuitRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    //IEnumerator DisplayMessage(string message)
    //{
    //    text.text = message;
    //    yield return new WaitForSeconds(2f);
    //    text.text = " ";
    //}
}
