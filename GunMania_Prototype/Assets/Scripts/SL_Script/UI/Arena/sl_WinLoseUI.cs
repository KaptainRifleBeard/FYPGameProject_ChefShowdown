using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using UnityEngine.SceneManagement;


public class sl_WinLoseUI : MonoBehaviourPunCallbacks
{
    public static readonly byte RestartEventCode = 1;
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

    void WinLoseCondition()
    {
        CheckIcon_p1();
        UiSize_p1();

        CheckIcon_p2();
        UiSize_p2();

        if (sl_PlayerHealth.currentHealth <= 0 || sl_MatchCountdown.timeRemaining == 0)
        {
            FindObjectOfType<sl_AudioManager>().Play("WinScreen");

            if (PhotonNetwork.IsMasterClient)
            {
                Nickname();

                //p1 lose
                winScreen.SetActive(true);

                if (sl_RematchAndLeave.rematchCount == 2)
                {
                    winScreen.SetActive(false);

                    sl_InventoryManager.ClearAllInList();
                    sl_p2InventoryManager.ClearAllInList();

                    sl_ShootBehavior.bulletCount = 0;
                    sl_P2ShootBehavior.p2bulletCount = 0;

                    sl_RematchAndLeave.rematchCount = 0;
                }
            }
            else
            {
                Nickname();

                //p2 win
                winScreen.SetActive(true);

                if (sl_RematchAndLeave.rematchCount == 2)
                {
                    winScreen.SetActive(false);

                    sl_InventoryManager.ClearAllInList();
                    sl_p2InventoryManager.ClearAllInList();

                    sl_ShootBehavior.bulletCount = 0;
                    sl_P2ShootBehavior.p2bulletCount = 0;

                    sl_RematchAndLeave.rematchCount = 0;
                }
            }
        }

        if (sl_P2PlayerHealth.p2currentHealth <= 0 || sl_MatchCountdown.timeRemaining == 0)
        {
            FindObjectOfType<sl_AudioManager>().Play("WinScreen");

            if (PhotonNetwork.IsMasterClient)
            {
                Nickname();

                //p1 win
                winScreen.SetActive(true);

                if (sl_RematchAndLeave.rematchCount == 2)
                {
                    winScreen.SetActive(false);

                    sl_InventoryManager.ClearAllInList();
                    sl_p2InventoryManager.ClearAllInList();

                    sl_ShootBehavior.bulletCount = 0;
                    sl_P2ShootBehavior.p2bulletCount = 0;

                    sl_RematchAndLeave.rematchCount = 0;
                }
            }
            else
            {
                Nickname();

                //p2 lose
                winScreen.SetActive(true);

                if (sl_RematchAndLeave.rematchCount == 2)
                {
                    winScreen.SetActive(false);

                    sl_InventoryManager.ClearAllInList();
                    sl_p2InventoryManager.ClearAllInList();

                    sl_ShootBehavior.bulletCount = 0;
                    sl_P2ShootBehavior.p2bulletCount = 0;

                    sl_RematchAndLeave.rematchCount = 0;
                }
            }
        }

    }


    //icon and name
    #region
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
        if (sl_PlayerHealth.currentHealth <= 0 || sl_PlayerHealth.currentHealth <= sl_P2PlayerHealth.p2currentHealth)
        {
            chamOrRunner1_text.text = "Runner-up";
            theUI_1.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        }
        else
        {
            chamOrRunner1_text.text = "Fling Champion";
            theUI_1.transform.localScale = new Vector3(1f, 1f, 1f);
        }

    }

    void UiSize_p2()
    {
        if (sl_P2PlayerHealth.p2currentHealth <= 0 || sl_P2PlayerHealth.p2currentHealth <= sl_PlayerHealth.currentHealth)
        {
            chamOrRunner2_text.text = "Runner-up";
            theUI_2.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        }
        else
        {
            chamOrRunner2_text.text = "Fling Champion";
            theUI_2.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    void Nickname()
    {
        player1Nickname.text = SL_newP1Movement.p1CurrentName;
        player2Nickname.text = sl_newP2Movement.p2CurrentName;

    }
    #endregion

    IEnumerator WaitStartGame()
    {
        yield return new WaitForSeconds(3.0f);
        WinLoseCondition();
    }

    IEnumerator ToExitScreen() //wait for animation
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