using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class sl_SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefabA;
    public GameObject playerPrefabB;

    public GameObject spawnPostionA;
    public GameObject spawnPostionB;

    void Start()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(playerPrefabA.name, spawnPostionA.transform.position, Quaternion.identity);

        }
        else
        {
            PhotonNetwork.Instantiate(playerPrefabB.name, spawnPostionB.transform.position, Quaternion.identity);
        }
    }


    void Update()
    {
        
    }
}
