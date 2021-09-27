using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class sl_LeaveRoomMenu : MonoBehaviour
{
    private sl_RoomCanvases roomCanvas;

    public void FirstInitialize(sl_RoomCanvases canvases)
    {
        roomCanvas = canvases;
    }


    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom(true);
        roomCanvas.CurrentRoomCanvas.Hide();
    }

}
