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

    void Start()
    {

        sl_P2PlayerHealth.player2Dead = false;

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

    }

}
