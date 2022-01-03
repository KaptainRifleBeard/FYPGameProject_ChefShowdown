using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine.SceneManagement;

public class sl_CreateAndJoinRoom : MonoBehaviourPunCallbacks
{
    public InputField createInput;

    [SerializeField]private Text roomName;
    RoomOptions options = new RoomOptions();

    private sl_RoomCanvases roomCanvas;
    public GameObject lobby; //the whole lobby 

    //for animation
    public GameObject canvasRoomListing; //tthe room listing part for animation
    public GameObject canvasCreateRoom; //the create room part for animation

    public Animator roomListAnim;
    public Animator createRoomAnim;

    private void Start()
    {

    }

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
        PhotonNetwork.LoadLevel("sl_PlayerRoom");

        //roomCanvas.CurrentRoomCanvas.Show();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room");
    }

    public void ShowLobby()
    {
        lobby.SetActive(true);
    }

    public void HideLobby()
    {
        lobby.SetActive(false);
    }



    //room's animation part
    //for hide and show in main menu
    public void ShowRoomListing()
    {
        StartCoroutine(CloseCreateRoomInput());

    }

    public void ShowCreateRoom()
    {
        StartCoroutine(OpenCreateRoomInput());
        roomListAnim.SetBool("OpenRoomListing", false);

    }

    //Create room animation
    IEnumerator OpenCreateRoomInput()
    {
        yield return new WaitForSeconds(0.5f);
        canvasCreateRoom.SetActive(true);

        createRoomAnim.SetBool("ShowCreateRoomInput", true);

    }

    IEnumerator CloseCreateRoomInput()
    {
        yield return new WaitForSeconds(0.5f);
        createRoomAnim.SetBool("ShowCreateRoomInput", false);
        roomListAnim.SetBool("OpenRoomListing", true);

    }

}
