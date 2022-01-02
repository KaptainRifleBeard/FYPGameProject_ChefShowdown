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
    //public static readonly byte RestartEventCode = 1;
    public Text player1Nickname;
    public Text player2Nickname;
    public Text chamOrRunner1_text;
    public Text chamOrRunner2_text;

    public GameObject winScreen;
    public GameObject theUI_1;
    public GameObject theUI_2;

    public Sprite[] icons;

    public Image player1CurrentIcon;
    public Image player2CurrentIcon;

    void Start()
    {
        winScreen.SetActive(false);
    }


    void Update()
    {
        StartCoroutine(WaitStartGame());


    }

    IEnumerator WaitStartGame()
    {
        yield return new WaitForSeconds(3.0f);
        WinLoseCondition();
    }

    void WinLoseCondition()
    {
        CheckIcon_p1();
        UiSize_p1();

        CheckIcon_p2();
        UiSize_p2();

        if (sl_PlayerHealth.currentHealth <= 0)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                Nickname();

                //p1 lose
                winScreen.SetActive(true);
                StartCoroutine(ToExitScreen());

            }
            else
            {
                Nickname();

                //p2 win
                winScreen.SetActive(true);
                StartCoroutine(ToExitScreen());

            }
        }

        if (sl_P2PlayerHealth.p2currentHealth <= 0)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                Nickname();

                //p1 win
                winScreen.SetActive(true);
                StartCoroutine(ToExitScreen());

            }
            else
            {
                Nickname();

                //p2 lose
                winScreen.SetActive(true);
                StartCoroutine(ToExitScreen());

            }
        }

    }

    void CheckIcon_p1()
    {
        //for icon
        if (SL_newP1Movement.changeModelAnim == 0) //brock
        {
            player1CurrentIcon.sprite = icons[0];
        }
        if (SL_newP1Movement.changeModelAnim == 1) //wen
        {
            player1CurrentIcon.sprite = icons[1];
        }
        if (SL_newP1Movement.changeModelAnim == 2) //jiho
        {
            player1CurrentIcon.sprite = icons[2];
        }
        if (SL_newP1Movement.changeModelAnim == 3) //katsuki
        {
            player1CurrentIcon.sprite = icons[3];
        }

    }

    void CheckIcon_p2()
    {
        //for icon
        if (sl_newP2Movement.changep2Icon == 0) //brock
        {
            player2CurrentIcon.sprite = icons[0];
        }
        if (sl_newP2Movement.changep2Icon == 1) //wen
        {
            player2CurrentIcon.sprite = icons[1];
        }
        if (sl_newP2Movement.changep2Icon == 2) //jiho
        {
            player2CurrentIcon.sprite = icons[2];
        }
        if (sl_newP2Movement.changep2Icon == 3) //katsuki
        {
            player2CurrentIcon.sprite = icons[3];
        }

    }

    void UiSize_p1()
    {
        //for text
        if (sl_PlayerHealth.currentHealth <= 0)
        {
            chamOrRunner1_text.text = "Runner-up";
            theUI_1.transform.localScale = new Vector3(2f, 2f, 2f);
        }
        else
        {
            chamOrRunner1_text.text = "Fling Champion";
            theUI_1.transform.localScale = new Vector3(2.6f, 2.6f, 2.6f);
        }

    }

    void UiSize_p2()
    {
        if (sl_P2PlayerHealth.p2currentHealth <= 0)
        {
            chamOrRunner2_text.text = "Runner-up";
            theUI_2.transform.localScale = new Vector3(2f, 2f, 2f);
        }
        else
        {
            chamOrRunner2_text.text = "Fling Champion";
            theUI_2.transform.localScale = new Vector3(2.6f, 2.6f, 2.6f);
        }
    }

    void Nickname()
    {
        player1Nickname.text = SL_newP1Movement.p1CurrentName;
        player2Nickname.text = sl_newP2Movement.p2CurrentName;

    }

    IEnumerator ToExitScreen()
    {
        yield return new WaitForSeconds(3.0f);
        //SceneManager.LoadScene("sl_BacktoMainMenu");

    }


}



//IEnumerator DisplayMessage(string message)
//{
//    text.text = message;
//    yield return new WaitForSeconds(2f);
//    text.text = " ";
//}

//public static void kick(int num) 
//{ 
//    foreach (Player player in PhotonNetwork.PlayerList) 
//    {
//        int s = player.ActorNumber;
//        if (s == num) 
//        { 
//            PhotonNetwork.CloseConnection(player); 
//            break; 
//        }
//    } 
//}