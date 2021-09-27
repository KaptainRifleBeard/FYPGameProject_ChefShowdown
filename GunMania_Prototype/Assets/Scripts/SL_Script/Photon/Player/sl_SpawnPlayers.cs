using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class sl_SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;

    public GameObject spawnPostionA;
    public GameObject spawnPostionB;

    void Start()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, spawnPostionA.transform.position, Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Instantiate(playerPrefab.name, spawnPostionB.transform.position, Quaternion.identity);
        }
    }


    void Update()
    {
        
    }
}
