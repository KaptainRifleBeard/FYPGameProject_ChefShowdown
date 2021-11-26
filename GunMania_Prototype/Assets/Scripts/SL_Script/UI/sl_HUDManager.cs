using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sl_HUDManager : MonoBehaviour
{
    public Sprite wenIcon;
    public Sprite brockIcon;

    //player1
    [Header("Player 1")]
    public GameObject brock;
    public GameObject tagBrock;

    //player2
    [Header("Player 2")]
    public GameObject wen;
    public GameObject tagWen;


    void Update()
    {
        if(SL_newP1Movement.changep1Icon == 0 || sl_SpawnPlayerManager.playerNum_p1 == 1)
        {
            //main brock side wen
            brock.GetComponent<Image>().sprite = brockIcon;
            tagBrock.GetComponent<Image>().sprite = wenIcon;
        }
        if (SL_newP1Movement.changep1Icon == 1 || sl_SpawnPlayerManager.playerNum_p1 == 2)
        {
            //main wen side brock
            brock.GetComponent<Image>().sprite = wenIcon;
            tagBrock.GetComponent<Image>().sprite = brockIcon;

        }
        if (SL_newP1Movement.changep1Icon == 2 || sl_SpawnPlayerManager.playerNum_p1 == 3) //3 and 4 use like brock and wen first
        {
            //main wen side brock
            brock.GetComponent<Image>().sprite = brockIcon;
            tagBrock.GetComponent<Image>().sprite = wenIcon;

        }
        if (SL_newP1Movement.changep1Icon == 3 || sl_SpawnPlayerManager.playerNum_p1 == 4)
        {
            //main wen side brock
            brock.GetComponent<Image>().sprite = wenIcon;
            tagBrock.GetComponent<Image>().sprite = brockIcon;

        }

        if (sl_newP2Movement.changep2Icon == 0 || sl_SpawnPlayerManager.playerNum_p2 == 1)
        {
            //main wen side brpck
            wen.GetComponent<Image>().sprite = wenIcon;
            tagWen.GetComponent<Image>().sprite = brockIcon;
        }
        if (sl_newP2Movement.changep2Icon == 1 || sl_SpawnPlayerManager.playerNum_p2 == 2)
        {
            //main brock side wen
            wen.GetComponent<Image>().sprite = brockIcon;
            tagWen.GetComponent<Image>().sprite = wenIcon;


        }
        if (sl_newP2Movement.changep2Icon == 2 || sl_SpawnPlayerManager.playerNum_p2 == 3) //3 and 4 use like brock and wen first
        {
            //main wen side brock
            brock.GetComponent<Image>().sprite = brockIcon;
            tagBrock.GetComponent<Image>().sprite = wenIcon;

        }
        if (sl_newP2Movement.changep2Icon == 3 || sl_SpawnPlayerManager.playerNum_p2 == 4)
        {
            //main wen side brock
            brock.GetComponent<Image>().sprite = wenIcon;
            tagBrock.GetComponent<Image>().sprite = brockIcon;

        }
    }
}
