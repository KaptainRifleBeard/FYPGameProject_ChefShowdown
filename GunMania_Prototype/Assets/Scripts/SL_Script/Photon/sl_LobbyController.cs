using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

public class sl_LobbyController : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject cancelButton;

    [SerializeField] private int roomSize;  //numbers of player in a room at one time

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        startButton.SetActive(true);
    }

    public void StartGame()
    {
        startButton.SetActive(false);
        cancelButton.SetActive(true);

        PhotonNetwork.JoinRandomRoom(); //first try to join existing room
        Debug.Log("Start!");

    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join a room");
        CreateRoom();
    }

    private void CreateRoom()  //create our own room
    {
        Debug.Log("Create room now");

        int randomRoomNumber = UnityEngine.Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)roomSize };

        PhotonNetwork.CreateRoom("Room " + randomRoomNumber, roomOps);
        Debug.Log("Room number " + randomRoomNumber);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create a room, try again.");
        CreateRoom();  //try to create new room
    }

    //Cancel start
    public void CancelButton()
    {
        cancelButton.SetActive(false);
        startButton.SetActive(true);

        PhotonNetwork.LeaveRoom();
    }




    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
