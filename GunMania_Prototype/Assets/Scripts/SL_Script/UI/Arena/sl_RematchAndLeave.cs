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

    //these is for function
    public static int rematchCount;
    public static bool leaveMatch;

    [Space(10)]
    [Header("P1")]
    public GameObject leaveButton;
    public GameObject rematchButton;
    public Button p1RButton;

    public GameObject p1_tick;

    [Space(10)]
    [Header("P2")]
    public GameObject p2_tick;

    public GameObject p1_checkbox;
    public GameObject p2_checkbox;

    void Start()
    {
        view = GetComponent<PhotonView>();

        p1_tick.SetActive(false);
        p2_tick.SetActive(false);

        leaveButton.SetActive(true);
        rematchButton.SetActive(true);

        rematchNum = 0;
        rematchCount = 0;

        leaveMatch = false;
        sl_MatchCountdown.timeRemaining = 5;
    }


    void Update()
    {
        if(rematchCount >= 2)
        {
            StartCoroutine(ResetCount());
        }

    }

    public void Rematch()
    {
        rematchCount += 1;

        if (PhotonNetwork.IsMasterClient)
        {
            rematchNum = 1; //for tick ui
        }
        else
        {
            rematchNum = 2;
        }

        view.RPC("SyncRematch", RpcTarget.All, rematchNum, rematchCount);
    }

    public void LeaveMatch()
    {
        rematchCount = 0;
        leaveMatch = true;

        if(PhotonNetwork.IsMasterClient)
        {
            rematchNum = 3; //for tick ui
        }
        else
        {
            rematchNum = 4;
        }

        //reset all 
        sl_P1CharacterSelect.numConfirm1 = 0;
        sl_P1CharacterSelect.numConfirm2 = 0;
        sl_P1CharacterSelect.p1_firstCharacter = 0;
        sl_P1CharacterSelect.p1_secondCharacter = 0;

        sl_P2CharacterSelect.p2_numConfirm1 = 0;
        sl_P2CharacterSelect.p2_numConfirm2 = 0;
        sl_P2CharacterSelect.p2_firstCharacter = 0;
        sl_P2CharacterSelect.p2_secondCharacter = 0;

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
            p1_checkbox.SetActive(false);

            p1RButton.interactable = false;
        }
        if (rematch == 4)
        {
            p2_tick.SetActive(false);
            p2_checkbox.SetActive(false);

            p1RButton.interactable = false;

        }
        #endregion

        if (rCount == 2)
        {
            PhotonNetwork.LoadLevel("sl_TestScene");
            sl_MatchCountdown.timeRemaining = 300;
        }

    }

    IEnumerator ResetCount()
    {
        yield return new WaitForSeconds(2.0f);
        rematchCount = 0;

    }

}
