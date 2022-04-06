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
    public GameObject erroMsg; 

    //for animation
    public GameObject canvasRoomListing; //tthe room listing part for animation
    public GameObject canvasCreateRoom; //the create room part for animation
    public GameObject roomListPos;
    public GameObject createRoomPos;

    public Animator roomListAnim;
    public Animator createRoomAnim;
    public Animator lobbyAnim;

    private void Start()
    {
        erroMsg.SetActive(false);

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

        if(createInput.text == "")
        {
            StartCoroutine(BlinkText());
        }
        else
        {
            options.MaxPlayers = 2;
            PhotonNetwork.JoinOrCreateRoom(createInput.text, options, null);
        }
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room Created");
        PhotonNetwork.LoadLevel("sl_NewPlayerRoom");

    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room");
    }

    public void ShowLobby()
    {
        //lobby.SetActive(true);
        lobbyAnim.SetBool("OpenLobby", true);
    }

    public void HideLobby()
    {
        //lobby.SetActive(false);
        lobbyAnim.SetBool("OpenLobby", false);
    }



    //room's animation part
    //for hide and show in main menu
    public void ShowRoomListing()
    {
        canvasRoomListing.transform.position = roomListPos.transform.position;
        StartCoroutine(CloseCreateRoomInput());

    }

    public void ShowCreateRoom()
    {
        Debug.Log("check");
        canvasCreateRoom.transform.position = createRoomPos.transform.position;

        StartCoroutine(OpenCreateRoomInput());
        roomListAnim.SetBool("OpenRoomListing", false);

    }

    //Create room animation
    IEnumerator OpenCreateRoomInput()
    {
        yield return new WaitForSeconds(0.2f);
        canvasRoomListing.transform.position = new Vector3(-54f, -293f, 0f);
        canvasCreateRoom.SetActive(true);

        createRoomAnim.SetBool("ShowCreateRoomInput", true);

    }

    IEnumerator CloseCreateRoomInput()
    {
        yield return new WaitForSeconds(0.2f);
        canvasCreateRoom.transform.position = new Vector3(-54f, -293f, 0f);

        createRoomAnim.SetBool("ShowCreateRoomInput", false);
        roomListAnim.SetBool("OpenRoomListing", true);

    }

    IEnumerator BlinkText()
    {
        erroMsg.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        erroMsg.SetActive(false);

    }

}
