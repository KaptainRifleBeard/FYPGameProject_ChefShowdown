using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class sl_SpawnPlayers : MonoBehaviour
{
    public GameObject spawnPostionA;
    public GameObject spawnPostionB;

    public GameObject brockChoi;
    public GameObject officerWen;

    GameObject player1;
    GameObject player2;

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            player1 = PhotonNetwork.Instantiate(brockChoi.name, spawnPostionA.transform.position, Quaternion.identity);
            player1.gameObject.tag = "Player";

        }
        else
        {
            player2 = PhotonNetwork.Instantiate(officerWen.name, spawnPostionB.transform.position, Quaternion.identity);
            player2.gameObject.tag = "Player2";

        }

    }

}
