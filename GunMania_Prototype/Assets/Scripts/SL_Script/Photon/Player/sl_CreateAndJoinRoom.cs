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

    [SerializeField]private Text roomName;
    RoomOptions options = new RoomOptions();

    private sl_RoomCanvases roomCanvas;

    public void FirstInitialize(sl_RoomCanvases canvases)
    {
        roomCanvas = canvases;
    }


    public void CreateRoom()
    {
        //if(PhotonNetwork.IsConnected)
        //{
        //    return;
        //}
        options.MaxPlayers = 2;

        PhotonNetwork.JoinOrCreateRoom(createInput.text, options, null);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room Created");
        roomCanvas.CurrentRoomCanvas.Show();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room");
    }

    //public void JoinRoom()
    //{
    //    PhotonNetwork.JoinRoom(joinInput.text);
    //}

    //public override void OnJoinedRoom()
    //{
    //    PhotonNetwork.LoadLevel("sl_PlayerSelectLobby");

    //}


}
