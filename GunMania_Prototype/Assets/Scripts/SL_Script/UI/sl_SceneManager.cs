using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using UnityEngine.SceneManagement;


public class sl_SceneManager : MonoBehaviour
{
    public void DisconnectPlayer()
    {
        //if (!PhotonNetwork.IsMasterClient)
        //{
        //    kick(2);
        //}
        //PhotonNetwork.LoadLevel(0);

        PhotonNetwork.LeaveRoom();
        StartCoroutine(DisconnectAndLoad());
        
    }

    IEnumerator DisconnectAndLoad()
    {
        PhotonNetwork.Disconnect(); //disconnect local player from master client
        while (PhotonNetwork.IsConnected)
        {
            yield return null;
        }

        SceneManager.LoadScene("sl_ServerLobby");

    }

}
