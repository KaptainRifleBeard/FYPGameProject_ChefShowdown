using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class sl_SpawnPlayers : MonoBehaviour
{
    public GameObject spawnPostionA;
    public GameObject spawnPostionB;

    public GameObject p1;
    public GameObject p2;

    GameObject player1;
    GameObject player2;

    public static int p1_StartModel;
    public static int p2_StartModel;

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            player1 = PhotonNetwork.Instantiate(p1.name, spawnPostionA.transform.position, Quaternion.identity);
            player1.gameObject.tag = "Player";
            player1.gameObject.layer = 7; //p1

        }
        else
        {
            player2 = PhotonNetwork.Instantiate(p2.name, spawnPostionB.transform.position, Quaternion.identity);
            player2.gameObject.tag = "Player2";
            player2.gameObject.layer = 8; //p2


        }


        //if (sl_SpawnPlayerManager.playerNum_p1 == 1)
        //{
        //    p1_StartModel = 1;
        //}
        //if (sl_SpawnPlayerManager.playerNum_p1 == 2)
        //{
        //    p1_StartModel = 2;
        //}
        //if (sl_SpawnPlayerManager.playerNum_p1 == 3)
        //{
        //    p1_StartModel = 3;
        //}
        //if (sl_SpawnPlayerManager.playerNum_p1 == 4)
        //{
        //    p1_StartModel = 4;
        //}


        //if (sl_SpawnPlayerManager.playerNum_p2 == 1)
        //{
        //    p2_StartModel = 1;
        //}
        //if (sl_SpawnPlayerManager.playerNum_p2 == 2)
        //{
        //    p2_StartModel = 2;
        //}
        //if (sl_SpawnPlayerManager.playerNum_p2 == 3)
        //{
        //    p2_StartModel = 3;
        //}
        //if (sl_SpawnPlayerManager.playerNum_p2 == 4)
        //{
        //    p2_StartModel = 4;
        //}
    }

}
