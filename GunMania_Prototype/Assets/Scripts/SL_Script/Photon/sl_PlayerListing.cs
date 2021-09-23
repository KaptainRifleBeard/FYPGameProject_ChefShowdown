using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;


public class sl_PlayerListing : MonoBehaviour
{
    [SerializeField] private Text text;

    public RoomInfo RoomInfo { get; private set; }
    public void SetRoomInfo(RoomInfo roomInfo)
    {
        RoomInfo = roomInfo;
        text.text = "Max player: " + roomInfo.MaxPlayers + " , " + roomInfo.Name;
    }

}
