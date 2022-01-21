using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using UnityEngine.SceneManagement;


public class sl_SceneManager : MonoBehaviourPunCallbacks
{
    public void DisconnectPlayer()
    {
        //if (!PhotonNetwork.IsMasterClient)
        //{
        //    kick(2);
        //}
        //PhotonNetwork.LoadLevel(0);

        PhotonNetwork.LeaveRoom(true);

        //StartCoroutine(DisconnectAndLoad());

    }

    IEnumerator DisconnectAndLoad()
    {
        PhotonNetwork.Disconnect(); //disconnect local player from master client
        while (PhotonNetwork.IsConnected)
        {
            yield return null;
        }

        SceneManager.LoadScene(0);

    }

    public override void OnLeftRoom()  //to avoid left room too quickly - MonoBehaviourPunCallbacks
    {
        SceneManager.LoadScene("sl_ServerLobby");

        base.OnLeftRoom();
    }
}
