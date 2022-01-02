using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class sl_CreateAndJoinRoom : MonoBehaviourPunCallbacks
{
    public InputField createInput;

    [SerializeField]private Text roomName;
    RoomOptions options = new RoomOptions();

    private sl_RoomCanvases roomCanvas;
    public GameObject lobby;

    public GameObject nickname;
    public GameObject canvasRoomListing;
    public GameObject canvasCreateRoom;
    public TextMeshProUGUI joinOrCreateText;

    public InputField placePlayerName;

    private void Start()
    {
        placePlayerName.interactable = false;
        placePlayerName.text = PhotonNetwork.NickName;
    }

    public void FirstInitialize(sl_RoomCanvases canvases)
    {
        roomCanvas = canvases;
        joinOrCreateText.text = " ";

    }

    public void CreateRoom()
    {
        options.MaxPlayers = 2;
        PhotonNetwork.JoinOrCreateRoom(createInput.text, options, null);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room Created");
        PhotonNetwork.LoadLevel("sl_PlayerRoom");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room");
    }

    public void StartGame() //for play button
    {
        //lobby.SetActive(true);
        //nickname.SetActive(true);

        PhotonNetwork.LoadLevel("sl_NameSetScene");

    }

    public void CloseLobby()
    {
        lobby.SetActive(false);
    }

    //for hide and show in main menu
    public void ShowRoomListing()
    {
        canvasRoomListing.SetActive(true);
        canvasCreateRoom.SetActive(false);

        joinOrCreateText.text = "Join a match";
    }

    public void ShowCreateRoom()
    {
        canvasRoomListing.SetActive(false);
        canvasCreateRoom.SetActive(true);
        joinOrCreateText.text = "Create a match";
    }

}
