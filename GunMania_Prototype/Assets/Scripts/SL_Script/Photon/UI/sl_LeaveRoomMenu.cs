using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class sl_LeaveRoomMenu : MonoBehaviourPunCallbacks
{
    private sl_RoomCanvases roomCanvas;

    public void FirstInitialize(sl_RoomCanvases canvases)
    {
        roomCanvas = canvases;
    }


    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom(true);
        //roomCanvas.CurrentRoomCanvas.Hide();
    }

    public override void OnLeftRoom()  //to avoid left room too quickly - MonoBehaviourPunCallbacks
    {
        SceneManager.LoadScene("sl_ServerLobby");

        base.OnLeftRoom();
    }

}
