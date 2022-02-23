using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class sl_RoomListing : MonoBehaviour
{
    [SerializeField] private Text text;

    public RoomInfo RoomInfo { get; private set; }
    public void SetRoomInfo(RoomInfo roomInfo)
    {
        RoomInfo = roomInfo;
        //text.text = "Max player: " + roomInfo.MaxPlayers + " , " + roomInfo.Name;
        text.text = roomInfo.Name + " (" + roomInfo.PlayerCount + "/" + roomInfo.MaxPlayers + ") ";

    }


    public void JoinLobby()
    {
        PhotonNetwork.JoinRoom(RoomInfo.Name);
    }

}
