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

    public GameObject canvasRoomListing;
    public GameObject canvasCreateRoom;

    public InputField placePlayerName;

    //for animation
    public Animator roomListAnim;
    public Animator createRoomAnim;

    private void Start()
    {
        placePlayerName.text = PhotonNetwork.NickName;
        canvasCreateRoom.SetActive(false);
    }

    public void FirstInitialize(sl_RoomCanvases canvases)
    {
        roomCanvas = canvases;

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

    public void CloseLobby()
    {
        lobby.SetActive(false);
    }

    //for hide and show in main menu
    public void ShowRoomListing()
    {
        //canvasRoomListing.SetActive(true);
        //canvasCreateRoom.SetActive(false);
        StartCoroutine(CloseCreateRoomInput());

    }

    public void ShowCreateRoom()
    {

        //canvasRoomListing.SetActive(false);
        //canvasCreateRoom.SetActive(true);

        StartCoroutine(OpenCreateRoomInput());
        roomListAnim.SetBool("OpenRoomListing", false);

    }




    //Create room animation
    IEnumerator OpenCreateRoomInput()
    {

        yield return new WaitForSeconds(0.8f);
        canvasCreateRoom.SetActive(true);
        createRoomAnim.SetBool("ShowCreateRoomInput", true);

    }

    IEnumerator CloseCreateRoomInput()
    {

        yield return new WaitForSeconds(0.8f);
        canvasCreateRoom.SetActive(false);
        createRoomAnim.SetBool("ShowCreateRoomInput", false);

        roomListAnim.SetBool("OpenRoomListing", true);

    }

}
