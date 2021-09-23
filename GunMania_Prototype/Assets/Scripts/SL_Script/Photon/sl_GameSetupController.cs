using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class sl_GameSetupController : MonoBehaviour
{
    void Start()
    {
        CreatePlayer();
    }

    public void CreatePlayer()
    {
        Debug.Log("Creating player...");
        PhotonNetwork.Instantiate(Path.Combine("PlayerCharacter", "PlayerCharacter"), Vector3.zero, Quaternion.identity);
    }
}
