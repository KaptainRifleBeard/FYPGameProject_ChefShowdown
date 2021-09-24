using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;

public class sl_CreateAndJoinRoom : MonoBehaviourPunCallbacks
{
    public InputField createInput;
    public InputField joinInput;

    [SerializeField]private Text roomName;
    RoomOptions options = new RoomOptions();

    int number;

    public void CreateRoom()
    {
        options.MaxPlayers = 2;

        PhotonNetwork.CreateRoom(createInput.text, options, null);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room Created");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room");
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("sl_TestScene");

    }


}
