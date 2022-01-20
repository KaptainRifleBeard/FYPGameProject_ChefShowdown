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

    public GameObject p1_tick;


    [Space(10)]
    [Header("P2")]
    public GameObject leaveButton2;
    public GameObject rematchButton2;

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
            rematchButton.SetActive(true);

            leaveButton2.SetActive(false);
            rematchButton2.SetActive(false);

        }
        else
        {
            leaveButton.SetActive(false);
            rematchButton.SetActive(false);

            leaveButton2.SetActive(true);
            rematchButton2.SetActive(true);

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

        view.RPC("SyncRematch", RpcTarget.All, rematchNum, rematchCount);

    }

    public void P2_Rematch()
    {
        rematchNum = 2;
        rematchCount += 1;

        view.RPC("SyncRematch", RpcTarget.All, rematchNum, rematchCount);

    }


    public void p1_LeaveMatch()
    {
        rematchNum = 3;
        rematchCount -= 1;

        //delete tick, p2 show only leave button on
        rematchButton2.SetActive(false);

        view.RPC("SyncRematch", RpcTarget.All, rematchNum, rematchCount);

    }

    public void p2_LeaveMatch()
    {
        rematchNum = 4;
        rematchCount -= 1;

        //delete tick, p2 show only leave button on
        rematchButton.SetActive(false);

        view.RPC("SyncRematch", RpcTarget.All, rematchNum, rematchCount);
    }

    [PunRPC]
    public void SyncRematch(int rematch, int rCount)
    {
        rematchNum = rematch;
        rematchCount = rCount;

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
    }

    IEnumerator ResetCount()
    {
        yield return new WaitForSeconds(2.0f);
        rematchCount = 0;
    }

}
