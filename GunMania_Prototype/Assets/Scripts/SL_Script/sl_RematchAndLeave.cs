using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class sl_RematchAndLeave : MonoBehaviour
{
    PhotonView view;

    //these num is just for ui sync
    int rematchNum; 
    int leaveNum;

    //these is for function
    public static int rematchCount;


    [Space(10)]
    [Header("P1")]
    public GameObject leaveButton;
    public GameObject rematchButton;
    public Button p1RButton;

    public GameObject p1_tick;


    [Space(10)]
    [Header("P2")]
    public GameObject leaveButton2;
    public GameObject rematchButton2;
    public Button p2RButton;

    public GameObject p2_tick;

    void Start()
    {
        view = GetComponent<PhotonView>();

        p1_tick.SetActive(false);
        p2_tick.SetActive(false);

        rematchNum = 0;
        leaveNum = 0;

        rematchCount = 0;


    }


    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            leaveButton.SetActive(true);

            leaveButton2.SetActive(false);
            rematchButton2.SetActive(false);

            if (leaveNum == 2)
            {
                rematchButton.SetActive(false);
                rematchButton2.SetActive(false);
            }
            else
            {
                rematchButton.SetActive(true);
            }
        }
        else
        {
            leaveButton.SetActive(false);
            rematchButton.SetActive(false);

            leaveButton2.SetActive(true);

            if (leaveNum == 1)
            {
                rematchButton.SetActive(false);
                rematchButton2.SetActive(false);
            }
            else
            {
                rematchButton2.SetActive(true);
            }
        }

        if(rematchCount >= 2)
        {
            StartCoroutine(ResetCount());
        }

    }

    public void P1_Rematch()
    {
        rematchNum = 1;
        rematchCount += 1;

        view.RPC("SyncRematch", RpcTarget.All, rematchNum, rematchCount, leaveNum);

    }

    public void P2_Rematch()
    {
        rematchNum = 2;
        rematchCount += 1;

        view.RPC("SyncRematch", RpcTarget.All, rematchNum, rematchCount, leaveNum);

    }


    public void p1_LeaveMatch()
    {
        leaveNum = 1;

        rematchNum = 3;
        rematchCount -= 1;

        view.RPC("SyncRematch", RpcTarget.All, rematchNum, rematchCount, leaveNum);

    }

    public void p2_LeaveMatch()
    {
        leaveNum = 2;
        rematchNum = 4;
        rematchCount -= 1;


        view.RPC("SyncRematch", RpcTarget.All, rematchNum, rematchCount, leaveNum);
    }

    [PunRPC]
    public void SyncRematch(int rematch, int rCount, int leave)
    {
        rematchNum = rematch;
        rematchCount = rCount;
        leaveNum = leave;

        //1, 2 = true; 3, 4 = false
        //rematch
        #region
        if(rematch == 1)
        {
            p1_tick.SetActive(true);
        }
        if(rematch == 2)
        {
            p2_tick.SetActive(true);
        }
        if (rematch == 3)
        {
            p1_tick.SetActive(false);
        }
        if (rematch == 4)
        {
            p2_tick.SetActive(false);
        }
        #endregion

        if(rCount == 2)
        {
            PhotonNetwork.LoadLevel("sl_TestScene");
        }

        if(leave == 1)
        {
            p1_tick.SetActive(false);
            rematchButton2.SetActive(false);
        }
        if(leave == 2)
        {
            p2_tick.SetActive(false);
            rematchButton.SetActive(false);
        }
    }

    IEnumerator ResetCount()
    {
        yield return new WaitForSeconds(2.0f);
        rematchCount = 0;

    }

}
