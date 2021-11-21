using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class sl_SpawnPlayerManager : MonoBehaviour
{
    public static int playerNum_p1; //1 = brock, 2 = wen
    public static int playerNum_p2; //1 = brock, 2 = wen

    public GameObject p1Button1;
    public GameObject p1Button2;
    public GameObject p1Brock;
    public GameObject p1Wen;

    public GameObject p2Button1;
    public GameObject p2Button2;
    public GameObject p2Brock;
    public GameObject p2Wen;

    PhotonView view;

    void Start()
    {
        playerNum_p1 = 1;
        playerNum_p2 = 1;

        view = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            p2Button1.SetActive(false);
            p2Button2.SetActive(false);

            p1Button1.SetActive(true);
            p1Button2.SetActive(true);
        }

        if (!PhotonNetwork.IsMasterClient)
        {
            p1Button1.SetActive(false);
            p1Button2.SetActive(false);

            p2Button1.SetActive(true);
            p2Button2.SetActive(true);

        }

    }

    public void Click_p1Brock()
    {
        //p1Brock.SetActive(false);
        //p1Wen.SetActive(true);
        view.RPC("P1ShowWen", RpcTarget.AllBufferedViaServer);

        playerNum_p1 = 2;

    }

    public void Click_p1Wen()
    {
        //p1Brock.SetActive(true);
        //p1Wen.SetActive(false);
        view.RPC("P1ShowBrock", RpcTarget.AllBufferedViaServer);

        playerNum_p1 = 1;
    }

    public void Click_p2Brock()
    {
        //p2Brock.SetActive(false);
        //p2Wen.SetActive(true);
        view.RPC("P2ShowWen", RpcTarget.All);

        playerNum_p2 = 2;
    }

    public void Click_p2Wen()
    {
        //p2Brock.SetActive(true);
        //p2Wen.SetActive(false);
        view.RPC("P2ShowBrock", RpcTarget.All);

        playerNum_p2 = 1;
    }


    [PunRPC]
    public void P1ShowBrock()
    {
        p1Brock.SetActive(true);
        p1Wen.SetActive(false);
    }

    [PunRPC]
    public void P1ShowWen()
    {
        p1Brock.SetActive(false);
        p1Wen.SetActive(true);
    }

    [PunRPC]
    public void P2ShowBrock()
    {
        p2Brock.SetActive(true);
        p2Wen.SetActive(false);
    }

    [PunRPC]
    public void P2ShowWen()
    {
        p2Brock.SetActive(false);
        p2Wen.SetActive(true);
    }
}
